using System.Data.Common;
using System.Threading.Tasks;
using Blurtle.Application;
using Blurtle.Domain;
using Dapper;

namespace Blurtle.Persistance {
    /// <summary>
    /// CRUD interface for storing posts.
    /// </summary>
    public sealed class PostRepo : DatabaseRepo<Post>, IPostRepo {
        #region Constructor(s)
        /// <summary>
        /// Create a new user repo.
        /// </summary>
        /// <param name="database">The database to query off.</param>
        public PostRepo(IDatabase database) : base(database) { }
        #endregion

        #region Publics
        public async Task<Post[]> FindNewest(int pageSize, int pageNumber) {
            using (DbConnection connection = GetConnection()) {
                return (await connection.QueryAsync<Post>(
                    "SELECT * FROM Post ORDER BY CreationDate DESC"
                )).AsList().ToArray();
            }
        }


        public async Task<Post> FindById(int id) {
            using (DbConnection connection = GetConnection()) {
                return await connection.QueryFirstAsync<Post>(
                    "SELECT * FROM Post WHERE Id = @Id;",
                    new { Id = id }
                );
            }
        }

        public async Task Add(Post post) {
            using (DbConnection connection = GetConnection()) {
                post.Id = await connection.QueryFirstOrDefaultAsync<int>(
                    "INSERT INTO Post (Title, Body, Type, CreationDate, UserId, WasEditted) VALUES (@Title, @Body, @Type, @CreationDate, @UserId, @WasEditted); SELECT LAST_INSERT_ID();",
                    post
                );
            }
        }

        public async Task Update(Post post) {
            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync("UPDATE Post SET Body = @Body WHERE Id = @Id", post);
            }
        }

        public async Task Delete(Post post) {
            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync("DELETE FROM Post WHERE Id = @Id", post);
            }
        }
        #endregion
    }
}