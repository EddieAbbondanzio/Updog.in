using System.Data.Common;
using System.Threading.Tasks;
using Updog.Application;
using Updog.Domain;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using System;

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
        /// <summary>
        /// Create a new comment repo.
        /// </summary>
        /// <param name="database">The active database.</param>
        /// <param name="commentMaper">Mapper to build comment entities.</param>
        public CommentRepo(IDatabase database, ICommentRecordMapper commentMapper) : base(database) {
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
                    "SELECT * FROM Comment LEFT JOIN User ON User.Id = Comment.UserId WHERE Comment.Id = @Id;",
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
        public async Task<Comment[]> FindByPost(int postId) {
            using (DbConnection connection = GetConnection()) {
                // Pull in every relevant comment first. This will be flat so we'll have to do some work.
                Comment[] comments = (await connection.QueryAsync<CommentRecord, UserRecord, Comment>(
                    "SELECT * FROM Comment LEFT JOIN User ON Comment.UserId = User.Id WHERE PostId = @PostId;",
                    (CommentRecord commentRec, UserRecord userRec) => {
                        return this.commentMapper.Map(Tuple.Create(commentRec, userRec));
                    },
                    new {
                        PostId = postId
                    })).ToArray();

                return BuildCommentTree(comments).ToArray();
            }
        }

        /// <summary>
        /// Add a new comment.
        /// </summary>
        /// <param name="entity">The comment to add.</param>
        public async Task Add(Comment entity) {
            CommentRecord commentRec = this.commentMapper.Reverse(entity).Item1;

            using (DbConnection connection = GetConnection()) {
                entity.Id = await connection.QueryFirstOrDefaultAsync<int>(
                    "INSERT INTO Comment (UserId, PostId, ParentId, Body, CreationDate, WasUpdated, WasDeleted) VALUES (@UserId, @PostId, @ParentId, @Body, @CreationDate, @WasUpdated, @WasDeleted); SELECT LAST_INSERT_ID();",
                    commentRec
                );
            }
        }

        /// <summary>
        /// Update an existing comment.
        /// </summary>
        /// <param name="entity">The comment to update.</param>
        public async Task Update(Comment entity) {
            CommentRecord commentRec = this.commentMapper.Reverse(entity).Item1;

            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync("UPDATE Comment SET Body = @Body WHERE Id = @Id", commentRec);
            }
        }

        /// <summary>
        /// Delete an existing comment.
        /// </summary>
        /// <param name="entity">The comment to delete.</param>
        public async Task Delete(Comment entity) {
            CommentRecord commentRec = this.commentMapper.Reverse(entity).Item1;

            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync("UPDATE Comment SET WasDeleted = TRUE Where Id = @Id", commentRec);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Build the comment tree(not really) from the flat array.
        /// </summary>
        /// <param name="flatComments">The comments before de-flattening.</param>
        /// <returns>The comments in hierarcheal order.</returns>
        private List<Comment> BuildCommentTree(Comment[] flatComments) {
            Dictionary<int, Comment> lookup = new Dictionary<int, Comment>();

            //Populate the lookup table
            for (int i = 0; i < flatComments.Length; i++) {
                lookup.Add(flatComments[i].Id, flatComments[i]);
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