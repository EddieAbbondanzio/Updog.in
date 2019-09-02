using System.Data.Common;
using System.Threading.Tasks;
using Updog.Application;
using Updog.Domain;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using System;
using Updog.Application.Paging;

namespace Updog.Persistance {
    /// <summary>
    /// CRUD interface for comments in the database.
    /// </summary>
    public sealed class CommentRepo : DatabaseRepo<Comment>, ICommentRepo {
        #region Fields
        /// <summary>
        /// The post repo.
        /// </summary>
        private IPostRepo postRepo;

        /// <summary>
        /// Mapper to convert comments into their record and back to entity.
        /// </summary>
        private ICommentRecordMapper commentMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment repo.
        /// </summary>
        /// <param name="database">The active database.</param>
        /// <param name="postRepo">The post repo</param>
        /// <param name="commentMaper">Mapper to build comment entities.</param>
        public CommentRepo(IDatabase database, IPostRepo postRepo, ICommentRecordMapper commentMapper) : base(database) {
            this.postRepo = postRepo;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a comment by ID.
        /// </summary>
        /// <param name="commentId">The ID of the comment.</param>
        /// <returns>The comment found.</returns>
        public async Task<Comment> FindById(int commentId) {
            // This doesn't pull in comment children and it should.
            using (DbConnection connection = GetConnection()) {
                Comment comment = (await connection.QueryAsync<CommentRecord, UserRecord, Comment>(
                    @"SELECT * FROM Comment LEFT JOIN ""User"" ON ""User"".Id = Comment.UserId WHERE Comment.Id = @Id ORDER BY CreationDate DESC",
                    (CommentRecord rec, UserRecord u) => {
                        return this.commentMapper.Map(Tuple.Create(rec, u));
                    },
                    new { Id = commentId }
                )).FirstOrDefault();

                return comment;
                // return BuildCommentTree(comments).ToArray();
            }
        }

        /// <summary>
        /// Find all comments for a post.
        /// </summary>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>It's children comments.</returns>
        public async Task<PagedResultSet<Comment>> FindByPost(int postId, int pageNumber, int pageSize) {
            Post p = await postRepo.FindById(postId);

            if (p == null) {
                return null;
            }

            using (DbConnection connection = GetConnection()) {
                // Pull in every relevant comment first. This will be flat so we'll have to do some work.
                IEnumerable<Comment> comments = (await connection.QueryAsync<CommentRecord, UserRecord, Comment>(
                    @"SELECT * FROM Comment LEFT JOIN ""User"" ON Comment.UserId = ""User"".Id WHERE PostId = @PostId ORDER BY CreationDate DESC LIMIT @Limit OFFSET @Offset",
                    (CommentRecord commentRec, UserRecord userRec) => {
                        Comment c = this.commentMapper.Map(Tuple.Create(commentRec, userRec));

                        //Set the parent post ref
                        c.Post = p;

                        return c;
                    },
                    BuildPaginationParams(
                        new { PostId = postId },
                        pageNumber,
                        pageSize
                    )
                ));

                //Get total count
                int totalCount = await connection.ExecuteScalarAsync<int>(
                    @"SELECT COUNT(*) FROM Comment WHERE PostId = @PostId",
                    new { PostId = postId }
                );

                List<Comment> tree = BuildCommentTree(comments);

                return new PagedResultSet<Comment>(tree, new PaginationInfo(pageNumber, pageSize, totalCount));
            }
        }

        /// <summary>
        /// Find a page of comments made by a specific user.
        /// </summary>
        /// <param name="username">The user to look for.</param>
        /// <param name="pageNumber">Page index.</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>The comments found.</returns>
        public async Task<PagedResultSet<Comment>> FindByUser(string username, int pageNumber, int pageSize) {
            using (DbConnection connection = GetConnection()) {
                IEnumerable<Comment> comments = await connection.QueryAsync<CommentRecord, UserRecord, Comment>(
                    @"SELECT * FROM Comment LEFT JOIN ""User"" ON Comment.UserId = ""User"".Id WHERE ""User"".Username = @Username ORDER BY CreationDate DESC LIMIT @Limit OFFSET @Offset ",
                    (commentRec, userRec) => {
                        return commentMapper.Map(Tuple.Create(commentRec, userRec));
                    },
                    BuildPaginationParams(
                        new { Username = username },
                        pageNumber,
                        pageSize
                    )
                );

                //Get total count
                int totalCount = await connection.ExecuteScalarAsync<int>(
                    @"SELECT COUNT(*) FROM Comment LEFT JOIN ""User"" ON Comment.UserId = ""User"".Id WHERE ""User"".Username = @Username",
                    new { Username = username }
                );

                return new PagedResultSet<Comment>(comments, new PaginationInfo(pageNumber, pageSize, totalCount));
            }
        }

        /// <summary>
        /// Add a new comment.
        /// </summary>
        /// <param name="entity">The comment to add.</param>
        public async Task Add(Comment entity) {
            CommentRecord commentRec = this.commentMapper.Reverse(entity).Item1;

            using (DbConnection connection = GetConnection()) {
                await connection.OpenAsync();

                using (DbTransaction transaction = connection.BeginTransaction()) {
                    entity.Id = await connection.QueryFirstOrDefaultAsync<int>(
                        @"INSERT INTO Comment (UserId, PostId, ParentId, Body, CreationDate, WasUpdated, WasDeleted) VALUES (@UserId, @PostId, @ParentId, @Body, @CreationDate, @WasUpdated, @WasDeleted) RETURNING Id;",
                        commentRec, transaction
                    );

                    //Update post comment count.
                    await connection.ExecuteAsync(@"UPDATE Post SET CommentCount = CommentCount + 1 WHERE Id = @Id", new { Id = entity.Post.Id }, transaction);

                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Update an existing comment.
        /// </summary>
        /// <param name="entity">The comment to update.</param>
        public async Task Update(Comment entity) {
            CommentRecord commentRec = this.commentMapper.Reverse(entity).Item1;

            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync(
                    @"UPDATE Comment SET 
                    UserId = @UserId, 
                    PostId = @PostId, 
                    ParentId = @ParentId, 
                    Body = @Body, 
                    CreationDate = @CreationDate, 
                    WasUpdated = @WasUpdated, 
                    WasDeleted = @WasDeleted 
                    WHERE Id = @Id",
                    commentRec
                );
            }
        }

        /// <summary>
        /// Delete an existing comment.
        /// </summary>
        /// <param name="entity">The comment to delete.</param>
        public async Task Delete(Comment entity) {
            CommentRecord commentRec = this.commentMapper.Reverse(entity).Item1;

            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync(@"UPDATE Comment SET WasDeleted = TRUE Where Id = @Id", commentRec);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Build the comment tree(not really) from the flat array.
        /// </summary>
        /// <param name="flatComments">The comments before de-flattening.</param>
        /// <returns>The comments in hierarcheal order.</returns>
        private List<Comment> BuildCommentTree(IEnumerable<Comment> flatComments) {
            Dictionary<int, Comment> lookup = new Dictionary<int, Comment>();

            //Populate the lookup table
            foreach (Comment c in flatComments) {
                lookup.Add(c.Id, c);
            }

            //Now iterate through the list and build the tree
            foreach (Comment c in lookup.Values) {
                if (c.Parent != null) {
                    Comment parent = lookup[c.Parent.Id];
                    parent.Children.Add(c);
                }
            }

            //Pull out the top level list.
            return lookup.Values.Where(c => c.Parent == null).ToList();
        }
        #endregion
    }
}