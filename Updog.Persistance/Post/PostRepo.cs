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
        /// <param name="commentRepo">The comment repo.</param>
        /// <param name="postMapper">The post entity mapper</param>
        public PostRepo(IDatabase database, IUserRepo userRepo, ICommentRepo commentRepo, IPostRecordMapper postMapper) : base(database) {
            this.userRepo = userRepo;
            this.commentRepo = commentRepo;
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
            User u = await userRepo.FindByEmail("FAKE");
            throw new Exception("FIX THIS DUMMY");
            // using (DbConnection connection = GetConnection()) {
            //     PostRecord[] records = (await connection.QueryAsync<PostRecord>(
            //         @"SELECT * FROM Post 
            //         ORDER BY CreationDate DESC
            //         LIMIT @PageSize
            //         OFFSET @Offset",
            //     new {
            //         PageSize = pagination.PageSize,
            //         Offset = pagination.GetOffset()
            //     })).ToArray();

            //     List<Post> posts = new List<Post>();

            //     for (int i = 0; i < records.Length; i++) {
            //         posts.Add(await MapRecordToPost(records[i]));
            //     }

            //     return posts.ToArray();
            // }
        }

        public async Task<Post> FindById(int id) {
            using (DbConnection connection = GetConnection()) {
                return (await connection.QueryAsync<PostRecord, UserRecord, Post>(
                    @"SELECT * FROM Post 
                    LEFT JOIN User ON Post.UserId = User.Id 
                    WHERE Post.Id = @Id;",
                    (PostRecord p, UserRecord u) => {
                        p.User = u;
                        return new PostRecordMapper(new UserRecordMapper()).Map(p);
                    },
                    new { Id = id }
                )).FirstOrDefault();

            }
        }

        public async Task Add(Post post) {
            using (DbConnection connection = GetConnection()) {
                post.Id = await connection.QueryFirstOrDefaultAsync<int>(
                    "INSERT INTO Post (Title, Body, Type, CreationDate, UserId, WasUpdated, WasDeleted) VALUES (@Title, @Body, @Type, @CreationDate, @UserId, @WasUpdated, @WasDeleted); SELECT LAST_INSERT_ID();",
                    postMapper.Reverse(post)
                );
            }
        }

        public async Task Update(Post post) {
            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync("UPDATE Post SET Body = @Body WHERE Id = @Id", postMapper.Reverse(post));
            }
        }

        public async Task Delete(Post post) {
            using (DbConnection connection = GetConnection()) {
                await connection.ExecuteAsync("UPDATE Post SET WasDeleted = TRUE WHERE Id = @Id", postMapper.Reverse(post));
            }
        }
        #endregion
    }
}