using System.Data.Common;
using System.Threading.Tasks;
using Updog.Application;
using Updog.Domain;
using Dapper;
using System.Linq;
using System.Collections.Generic;

namespace Updog.Persistance {
    /// <summary>
    /// CRUD interface for comments in the database.
    /// </summary>
    public sealed class CommentRepo : DatabaseRepo<Comment>, ICommentRepo {
        #region Fields
        /// <summary>
        /// The user repo.
        /// </summary>
        private IUserRepo userRepo;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment repo.
        /// </summary>
        /// <param name="database">The active database.</param>
        /// <param name="userRepo">CRUD interface for users.</param>
        /// <returns></returns>
        public CommentRepo(IDatabase database, IUserRepo userRepo) : base(database) {
            this.userRepo = userRepo;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a comment by ID.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        /// <returns>The comment found.</returns>
        public async Task<Comment> FindById(int id) {
            using (DbConnection connection = GetConnection()) {
                CommentRecord record = await connection.QuerySingleOrDefaultAsync<CommentRecord>(
                    "SELECT * FROM Comment WHERE Id = @Id;",
                    new { Id = id }
                );

                return await MapRecordToEntity(record);
            }
        }

        /// <summary>
        /// Find all comments for a post.
        /// </summary>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>It's children comments.</returns>
        public async Task<Comment[]> FindByPost(int postId) {
            using (DbConnection connection = GetConnection()) {
                CommentRecord[] records = (await connection.QueryAsync<CommentRecord>("SELECT * FROM Comment")).ToArray();
                List<Comment> comments = new List<Comment>();

                for (int i = 0; i < records.Length; i++) {
                    comments.Add(await MapRecordToEntity(records[i]));
                }

                return comments.ToArray();
            }
        }

        /// <summary>
        /// Add a new comment.
        /// </summary>
        /// <param name="entity">The comment to add.</param>
        public async Task Add(Comment entity) {
            using (DbConnection connection = GetConnection()) {
                entity.Id = await connection.QueryFirstOrDefaultAsync<int>(
                    "INSERT INTO Comment (UserId, PostId, ParentId, Body, CreationDate, WasUpdated, WasDeleted) VALUES (@UserId, @PostId, @ParentId, @Body, @CreationDate, @WasUpdated, @WasDeleted); SELECT LAST_INSERT_ID();",
                    entity
                );
            }
        }

        /// <summary>
        /// Update an existing comment.
        /// </summary>
        /// <param name="entity">The comment to update.</param>
        public async Task Update(Comment entity) {
            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync("UPDATE Comment SET Body = @Body WHERE Id = @Id", entity);
            }
        }

        /// <summary>
        /// Delete an existing comment.
        /// </summary>
        /// <param name="entity">The comment to delete.</param>
        public async Task Delete(Comment entity) {
            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync("UPDATE Comment SET WasDeleted = TRUE Where Id = @Id", entity);
            }
        }
        #endregion

        #region Helpers
        private async Task<Comment> MapRecordToEntity(CommentRecord record) {
            User user = await userRepo.FindById(record.UserId);

            Comment c = new Comment() {
                Id = record.Id,
                User = user,
                Body = record.Body,
                CreationDate = record.CreationDate,
                WasUpdated = record.WasUpdated,
                WasDeleted = record.WasDeleted
            };

            if (record.ParentId != 0) {
                c.Parent = await FindById(record.ParentId);
            }

            return c;
        }
        #endregion
    }
}