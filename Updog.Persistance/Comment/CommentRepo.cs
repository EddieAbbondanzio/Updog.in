using System.Data.Common;
using System.Threading.Tasks;
using Updog.Application;
using Updog.Domain;
using Dapper;
using System.Linq;

namespace Updog.Persistance {
    /// <summary>
    /// CRUD interface for comments in the database.
    /// </summary>
    public sealed class CommentRepo : DatabaseRepo<Comment>, ICommentRepo {
        #region Constructor(s)
        public CommentRepo(IDatabase database) : base(database) { }
        #endregion

        #region Publics
        /// <summary>
        /// Find a comment by ID.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        /// <returns>The comment found.</returns>
        public async Task<Comment> FindById(int id) {
            using (DbConnection connection = GetConnection()) {
                return await connection.QueryFirstAsync<Comment>(
                    "SELECT * FROM Comment WHERE Id = @Id;",
                    new { Id = id }
                );
            }
        }

        /// <summary>
        /// Find all comments for a post.
        /// </summary>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>It's children comments.</returns>
        public async Task<CommentInfo[]> FindCommentsByPost(int postId) {
            using (DbConnection connection = GetConnection()) {
                return (await connection.QueryAsync<Comment, User, CommentInfo>(
                    @"SELECT Comment.Id, Comment.UserId, Comment.PostId, Comment.ParentId, Comment.Body, Comment.CreationDate, Comment.WasUpdated, Comment.WasDeleted, User.Id as UId, User.Username
                    FROM Comment
                    LEFT JOIN User ON Comment.UserId = User.Id",
                    (c, u) => { return new CommentInfo(c.Id, u.Username, c.Body); },
                    splitOn: "UId")
                ).ToArray();
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
    }
}