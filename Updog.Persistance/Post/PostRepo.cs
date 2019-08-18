using System.Data.Common;
using System.Threading.Tasks;
using Updog.Application;
using Updog.Domain;
using Dapper;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Updog.Persistance {
    /// <summary>
    /// CRUD interface for storing posts.
    /// </summary>
    public sealed class PostRepo : DatabaseRepo<Post>, IPostRepo {
        #region Fields
        private IUserRepo userRepo;
        private ICommentRepo commentRepo;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post repo.
        /// </summary>
        /// <param name="database">The active database.</param>
        /// <param name="userRepo">The user repo.</param>
        /// <param name="commentRepo">The comment repo.</param>
        public PostRepo(IDatabase database, IUserRepo userRepo, ICommentRepo commentRepo) : base(database) {
            this.userRepo = userRepo;
            this.commentRepo = commentRepo;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find the newest posts by their creation date.
        /// </summary>
        /// <param name="pagination">The paging info.</param>
        /// <returns></returns>
        public async Task<Post[]> FindNewest(PaginationInfo pagination) {
            using (DbConnection connection = GetConnection()) {
                PostRecord[] records = (await connection.QueryAsync<PostRecord>(
                    @"SELECT * FROM Post 
                    ORDER BY CreationDate DESC
                    LIMIT @PageSize
                    OFFSET @Offset",
                new {
                    PageSize = pagination.PageSize,
                    Offset = pagination.GetOffset()
                })).ToArray();

                List<Post> posts = new List<Post>();

                for (int i = 0; i < records.Length; i++) {
                    posts.Add(await MapRecordToPost(records[i]));
                }

                return posts.ToArray();
            }
        }

        public async Task<Post> FindById(int id) {
            using (DbConnection connection = GetConnection()) {
                PostRecord record = await connection.QuerySingleOrDefaultAsync<PostRecord>(
                    "SELECT * FROM Post WHERE Id = @Id;",
                    new { Id = id }
                );

                return await MapRecordToPost(record);
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

        #region Helpers
        /// <summary>
        /// Helper to convert a record to a post.
        /// </summary>
        /// <param name="record">The post record to convert.</param>
        /// <returns>The rebuilt post entity.</returns>
        private async Task<Post> MapRecordToPost(PostRecord record) {
            User user = await userRepo.FindById(record.UserId);
            Comment[] comments = await commentRepo.FindByPost(record.Id);

            return new Post() {
                Id = record.Id,
                User = user,
                Type = record.Type,
                Title = record.Title,
                Body = record.Body,
                CreationDate = record.CreationDate,
                WasUpdated = record.WasUpdated,
                WasDeleted = record.WasDeleted,
                Comments = comments
            };
        }
        #endregion
    }
}