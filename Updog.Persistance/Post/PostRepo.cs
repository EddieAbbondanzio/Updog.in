using System.Data.Common;
using System.Threading.Tasks;
using Updog.Application;
using Updog.Domain;
using Dapper;
using System.Linq;
using System;

namespace Updog.Persistance {
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
        /// <summary>
        /// Find the newest posts by their creation date.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// /// <returns></returns>
        public async Task<PostInfo[]> FindNewest(int pageNumber, int pageSize) {
            using (DbConnection connection = GetConnection()) {
                return (await connection.QueryAsync<Post, User, PostInfo>(
                    @"SELECT Post.Id, Post.Type, Post.Type, Post.Title, Post.Body, Post.UserId, Post.CreationDate, User.Id as UId, User.Username 
                        FROM Post 
                        LEFT JOIN User ON Post.UserId = User.Id 
                        ORDER BY CreationDate DESC
                        LIMIT @PageSize
                        OFFSET @Offset",
                    (p, u) => {
                        return new PostInfo(p.Id, p.Type, p.Title, p.Body, u.Username, p.CreationDate);
                    },
                    new {
                        PageSize = pageSize,
                        Offset = pageNumber * pageSize
                    },
                    splitOn: "UId")
                ).ToArray();
            }
        }

        public async Task<Post> FindById(int id) {
            using (DbConnection connection = GetConnection()) {
                return await connection.QuerySingleOrDefaultAsync<Post>(
                    "SELECT * FROM Post WHERE Id = @Id;",
                    new { Id = id }
                );
            }
        }

        public async Task Add(Post post) {
            using (DbConnection connection = GetConnection()) {
                post.Id = await connection.QueryFirstOrDefaultAsync<int>(
                    "INSERT INTO Post (Title, Body, Type, CreationDate, UserId, WasUpdated, WasDeleted) VALUES (@Title, @Body, @Type, @CreationDate, @UserId, @WasUpdated, @WasDeleted); SELECT LAST_INSERT_ID();",
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
                await connection.ExecuteAsync("UPDATE Post SET WasDeleted = TRUE WHERE Id = @Id", post);
            }
        }
        #endregion
    }
}