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
        /// Mapper to convert comments into their record and back to entity.
        /// </summary>
        private ICommentRecordMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentRepo(DbConnection connection) : base(connection) {
            this.commentMapper = new CommentRecordMapper(new UserRecordMapper());
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a comment by ID.
        /// </summary>
        /// <param name="commentId">The ID of the comment.</param>
        /// <returns>The comment found.</returns>
        public async Task<Comment?> FindById(int commentId) {
            // This doesn't pull in comment children and it should.
            IEnumerable<Comment> comments = await Connection.QueryAsync<CommentRecord, UserRecord, Comment>(
                @"WITH RECURSIVE commenttree AS (
                    SELECT r.* FROM Comment r WHERE Id = @Id 
                    UNION ALL
                    SELECT c.* FROM Comment c
                    INNER JOIN commenttree ct ON ct.Id = c.ParentId
                    ) SELECT * FROM commenttree
                    LEFT JOIN ""User"" ON UserId = ""User"".Id ORDER BY ParentId, CreationDate ASC;",
                Mapper,
                new { Id = commentId }
            );

            if (comments.Count() == 0) {
                return null;
            }

            return BuildCommentTree(comments, commentId)[0];
        }

        /// <summary>
        /// Find all comments for a post.
        /// </summary>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>It's children comments.</returns>
        public async Task<IEnumerable<Comment>> FindByPost(int postId) {
            // Pull in every relevant comment first. This will be flat so we'll have to do some work.
            IEnumerable<Comment> comments = (await Connection.QueryAsync<CommentRecord, UserRecord, Comment>(
                @"
                    WITH RECURSIVE commenttree AS (
                    SELECT r.* FROM Comment r WHERE PostId = @PostId AND ParentId = 0 AND IsDeleted = FALSE 
                    UNION ALL
                    SELECT c.* FROM Comment c
                    INNER JOIN commenttree ct ON ct.Id = c.ParentId
                    ) SELECT * FROM commenttree
                    LEFT JOIN ""User"" ON UserId = ""User"".Id 
                    ORDER BY ParentId, CreationDate ASC;",
                Mapper,
                new { PostId = postId }
            ));

            List<Comment> tree = BuildCommentTree(comments);
            return tree;
        }

        /// <summary>
        /// Find a page of comments made by a specific user.
        /// </summary>
        /// <param name="username">The user to look for.</param>
        /// <param name="pageNumber">Page index.</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>The comments found.</returns>
        public async Task<PagedResultSet<Comment>> FindByUser(string username, int pageNumber, int pageSize) {
            IEnumerable<Comment> comments = await Connection.QueryAsync<CommentRecord, UserRecord, Comment>(
                @"SELECT * FROM Comment 
                    LEFT JOIN ""User"" ON Comment.UserId = ""User"".Id 
                    WHERE ""User"".Username = @Username AND IsDeleted = FALSE 
                    ORDER BY CreationDate DESC 
                    LIMIT @Limit 
                    OFFSET @Offset ",
                Mapper,
                BuildPaginationParams(
                    new { Username = username },
                    pageNumber,
                    pageSize
                )
            );

            //Get total count
            int totalCount = await Connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM Comment 
                    LEFT JOIN ""User"" ON Comment.UserId = ""User"".Id 
                    WHERE ""User"".Username = @Username AND IsDeleted = FALSE",
                new { Username = username }
            );

            return new PagedResultSet<Comment>(comments, new PaginationInfo(pageNumber, pageSize, totalCount));
        }

        /// <summary>
        /// Add a new comment.
        /// </summary>
        /// <param name="entity">The comment to add.</param>
        public async Task Add(Comment entity) {
            CommentRecord commentRec = this.commentMapper.Reverse(entity).Item1;

            entity.Id = await Connection.QueryFirstOrDefaultAsync<int>(
                @"INSERT INTO Comment (UserId, PostId, ParentId, Body, CreationDate, WasUpdated, WasDeleted, Upvotes, Downvotes) 
                    VALUES (@UserId, @PostId, @ParentId, @Body, @CreationDate, @WasUpdated, @WasDeleted, @Upvotes, @Downvotes) RETURNING Id;",
                commentRec
            );
        }

        /// <summary>
        /// Update an existing comment.
        /// </summary>
        /// <param name="entity">The comment to update.</param>
        public async Task Update(Comment entity) {
            CommentRecord commentRec = this.commentMapper.Reverse(entity).Item1;

            await Connection.ExecuteAsync(
                @"UPDATE Comment SET 
                    UserId = @UserId, 
                    PostId = @PostId, 
                    ParentId = @ParentId, 
                    Body = @Body, 
                    CreationDate = @CreationDate, 
                    WasUpdated = @WasUpdated, 
                    WasDeleted = @WasDeleted,
                    Upvotes = @Upvotes,
                    Downvotes = @Downvotes
                    WHERE Id = @Id",
                commentRec
            );

            entity.WasUpdated = true;
        }

        /// <summary>
        /// Delete an existing comment.
        /// </summary>
        /// <param name="entity">The comment to delete.</param>
        public async Task Delete(Comment entity) {
            CommentRecord commentRec = this.commentMapper.Reverse(entity).Item1;
            await Connection.ExecuteAsync(@"UPDATE Comment SET WasDeleted = TRUE Where Id = @Id", commentRec);

            entity.WasDeleted = true;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Build the comment tree(not really) from the flat array.
        /// </summary>
        /// <param name="flatComments">The comments before de-flattening.</param>
        /// <returns>The comments in hierarcheal order.</returns>
        private List<Comment> BuildCommentTree(IEnumerable<Comment> flatComments, int rootId = 0) {
            Dictionary<int, Comment> lookup = new Dictionary<int, Comment>();

            //Populate the lookup table
            foreach (Comment c in flatComments) {
                lookup.Add(c.Id, c);
            }

            //Now iterate through the list and build the tree
            foreach (Comment c in lookup.Values) {
                if (c.Parent != null) {

                    if (lookup.ContainsKey(c.Parent.Id)) {
                        Comment parent = lookup[c.Parent.Id];
                        parent.Children.Add(c);
                    }
                }
            }

            if (rootId != 0) {
                return lookup.Values.Where(c => c.Id == rootId).ToList();
            } else {
                //Pull out the top level list.
                return lookup.Values.Where(c => c.Parent == null).ToList();
            }
        }

        private Comment Mapper(CommentRecord commentRec, UserRecord userRec) => this.commentMapper.Map(Tuple.Create(commentRec, userRec));
        #endregion
    }
}