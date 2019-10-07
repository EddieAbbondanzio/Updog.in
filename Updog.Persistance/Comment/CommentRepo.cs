using System.Data.Common;
using System.Threading.Tasks;
using Updog.Application;
using Updog.Domain;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using System;
using Updog.Domain.Paging;

namespace Updog.Persistance {
    /// <summary>
    /// CRUD interface for comments in the database.
    /// </summary>
    public sealed class CommentRepo : DatabaseRepo<Comment>, ICommentRepo {
        #region Fields
        /// <summary>
        /// Mapper to convert comments into their record and back to entity.
        /// </summary>
        private ICommentMapper mapper;
        #endregion

        #region Constructor(s)
        public CommentRepo(IDatabase database, ICommentMapper mapper) : base(database) {
            this.mapper = mapper;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a comment by ID.
        /// </summary>
        /// <param name="commentId">The ID of the comment.</param>
        /// <returns>The comment found.</returns>
        public override async Task<Comment?> FindById(int commentId) {
            var comments = await Connection.QueryAsync<CommentRecord>(
                @"WITH RECURSIVE commenttree AS (
                    SELECT r.* FROM Comment r WHERE Id = @Id AND WasDeleted = FALSE
                    UNION ALL
                    SELECT c.* FROM Comment c
                    INNER JOIN commenttree ct ON ct.Id = c.ParentId
                    WHERE c.WasDeleted = FALSE
                    ) SELECT * FROM commenttree
                    LEFT JOIN ""User"" ON UserId = ""User"".Id ORDER BY ParentId, CreationDate ASC;",
                new { Id = commentId }
            );

            if (comments.Count() == 0) {
                return null;
            }

            return mapper.Map(comments.ElementAt(0));
        }

        /// <summary>
        /// Add a new comment.
        /// </summary>
        /// <param name="entity">The comment to add.</param>
        public override async Task Add(Comment entity) {
            CommentRecord commentRec = this.mapper.Reverse(entity);

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
        public override async Task Update(Comment entity) {
            CommentRecord commentRec = this.mapper.Reverse(entity);

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
        }

        /// <summary>
        /// Delete an existing comment.
        /// </summary>
        /// <param name="entity">The comment to delete.</param>
        public override async Task Delete(Comment entity) {
            CommentRecord commentRec = this.mapper.Reverse(entity);
            await Connection.ExecuteAsync(@"UPDATE Comment SET WasDeleted = TRUE Where Id = @Id", commentRec);
        }
        #endregion
    }
}