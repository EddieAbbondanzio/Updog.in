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
        private ICommentFactory factory;
        #endregion

        #region Constructor(s)
        private CommentRepo(IDatabase database, ICommentFactory factory) : base(database) {
            this.factory = factory;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a comment by ID.
        /// </summary>
        /// <param name="commentId">The ID of the comment.</param>
        /// <returns>The comment found.</returns>
        public override async Task<Comment?> FindById(int commentId) {
            var comments = (await Connection.QueryAsync<CommentRecord>(
                @"WITH RECURSIVE commenttree AS (
                    SELECT r.* FROM comment r WHERE id = @Id AND was_deleted = FALSE
                    UNION ALL
                    SELECT c.* FROM comment c
                    INNER JOIN commenttree ct ON ct.id = c.parent_id
                    WHERE c.was_deleted = FALSE
                    ) SELECT * FROM commenttree
                    LEFT JOIN ""user"" ON user_id = ""user"".id ORDER BY parent_id, creation_date ASC;",
                new { Id = commentId }
            )).Select(Map);

            return comments.ElementAtOrDefault(0);
        }

        /// <summary>
        /// Add a new comment.
        /// </summary>
        /// <param name="entity">The comment to add.</param>
        public override async Task Add(Comment entity) => entity.Id = await Connection.QueryFirstOrDefaultAsync<int>(
                @"INSERT INTO comment (user_id, post_id, parent_id, body, creation_date, was_updated, was_deleted, upvotes, downvotes) 
                    VALUES (@UserId, @PostId, @ParentId, @Body, @CreationDate, @WasUpdated, @WasDeleted, @Upvotes, @Downvotes) RETURNING Id;",
                Reverse(entity)
            );

        /// <summary>
        /// Update an existing comment.
        /// </summary>
        /// <param name="entity">The comment to update.</param>
        public override async Task Update(Comment entity) => await Connection.ExecuteAsync(
                @"UPDATE Comment SET 
                    user_id = @UserId, 
                    post_id = @PostId, 
                    parent_id = @ParentId, 
                    body = @Body, 
                    creation_date = @CreationDate, 
                    was_updated = @WasUpdated, 
                    was_deleted = @WasDeleted,
                    upvotes = @Upvotes,
                    downvotes = @Downvotes
                    WHERE id = @Id",
                Reverse(entity)
            );

        /// <summary>
        /// Delete an existing comment.
        /// </summary>
        /// <param name="entity">The comment to delete.</param>
        public override async Task Delete(Comment entity) => await Connection.ExecuteAsync(@"UPDATE comment SET was_deleted = TRUE Where id = @Id", Reverse(entity));


        public async Task<bool> IsOwner(int commentId, string username) {
            var owner = await Connection.ExecuteScalarAsync<string>(
                @"SELECT u.username FROM comment c 
                    JOIN ""user"" u ON u.id = c.user_id 
                    WHERE c.id = @Id",
                new { Id = commentId });
            return owner == username;
        }
        #endregion

        #region Privates
        private Comment Map(CommentRecord rec) => factory.Create(
            rec.Id,
            rec.UserId,
            rec.PostId,
            rec.ParentId,
            rec.Body,
            new VoteStats(rec.Upvotes, rec.Downvotes), rec.CreationDate, rec.WasUpdated, rec.WasDeleted);

        private CommentRecord Reverse(Comment comment) => new CommentRecord() {
            Id = comment.Id,
            UserId = comment.UserId,
            PostId = comment.PostId,
            ParentId = comment.ParentId,
            Body = comment.Body,
            CreationDate = comment.CreationDate,
            WasUpdated = comment.WasUpdated,
            WasDeleted = comment.WasDeleted,
            Upvotes = comment.Votes.Upvotes,
            Downvotes = comment.Votes.Downvotes
        };
        #endregion
    }
}