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

        /// <summary>
        /// Mapper to convert the post record into its entity.
        /// </summary>
        private IPostRecordMapper postMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post repo.
        /// </summary>
        /// <param name="database">The active database.</param>
        /// <param name="userRepo">The user repo.</param>
        /// <param name="postMapper">The post entity mapper</param>
        public PostRepo(IDatabase database, IUserRepo userRepo, IPostRecordMapper postMapper) : base(database) {
            this.userRepo = userRepo;
            this.postMapper = postMapper;
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
                return (await connection.QueryAsync<PostRecord, UserRecord, Post>(
                    @"SELECT * FROM Post
                    LEFT JOIN User ON Post.UserId = User.Id
                    ORDER BY CreationDate ASC
                    LIMIT = @Limit
                    OFFSET = @Offset",
                    (PostRecord postRec, UserRecord userRec) => {
                        return postMapper.Map(Tuple.Create(postRec, userRec));
                    }
                    , new {
                        Limit = pagination.PageSize,
                        Offset = pagination.GetOffset()
                    }
                )).ToArray();
            }
        }

        public async Task<Post> FindById(int id) {
            using (DbConnection connection = GetConnection()) {
                return (await connection.QueryAsync<PostRecord, UserRecord, Post>(
                    @"SELECT * FROM Post 
                    LEFT JOIN User ON Post.UserId = User.Id 
                    WHERE Post.Id = @Id;",
                    (PostRecord p, UserRecord u) => {
                        return postMapper.Map(Tuple.Create(p, u));
                    },
                    new { Id = id }
                )).FirstOrDefault();

            }
        }

        public async Task Add(Post post) {
            using (DbConnection connection = GetConnection()) {
                post.Id = await connection.QueryFirstOrDefaultAsync<int>(
                    "INSERT INTO Post (Title, Body, Type, CreationDate, UserId, WasUpdated, WasDeleted) VALUES (@Title, @Body, @Type, @CreationDate, @UserId, @WasUpdated, @WasDeleted); SELECT LAST_INSERT_ID();",
                    postMapper.Reverse(post).Item1
                );
            }
        }

        public async Task Update(Post post) {
            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync("UPDATE Post SET Body = @Body WHERE Id = @Id", postMapper.Reverse(post).Item1);
            }
        }

        public async Task Delete(Post post) {
            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync("UPDATE Post SET WasDeleted = TRUE WHERE Id = @Id", postMapper.Reverse(post).Item1);
            }
        }
        #endregion
    }
}