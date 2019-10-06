using System.Data.Common;
using System.Threading.Tasks;
using Updog.Application;
using Updog.Domain;
using Dapper;
using System.Linq;
using System;
using System.Collections.Generic;
using Updog.Domain.Paging;

namespace Updog.Persistance {
    /// <summary>
    /// CRUD interface for storing posts.
    /// </summary>
    public sealed class PostRepo : DapperRepo<Post>, IPostRepo {
        #region Fields
        /// <summary>
        /// Mapper to convert the post record into its entity.
        /// </summary>
        private IPostRecordMapper mapper;
        #endregion

        #region Constructor(s)
        public PostRepo(IDatabase database) : base(database) {
            this.mapper = new PostRecordMapper(new UserRecordMapper(), new SpaceRecordMapper());
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find the newest posts by their creation date.
        /// </summary>
        /// <param name="pageNumber">Index of the page..</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>The result set.</returns>
        public async Task<PagedResultSet<Post>> FindNewest(int pageNumber, int pageSize) {
            var posts = await Connection.QueryAsync<PostRecord>(
                @"SELECT * FROM Post
                    ORDER BY Post.CreationDate DESC
                    WHERE WasDeleted = FALSE
                    LIMIT @Limit
                    OFFSET @Offset",
                BuildPaginationParams(pageNumber, pageSize)
            );

            //Get total count
            int totalCount = await Connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Post;"
            );

            return new PagedResultSet<Post>(posts.Select(p => mapper.Map(p)), new PaginationInfo(pageNumber, pageSize, totalCount));
        }

        /// <summary>
        /// Find posts for a specific user.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <param name="pageNumber">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The collection of their posts (if any).</returns>

        public async Task<PagedResultSet<Post>> FindByUser(string username, int pageNumber, int pageSize) {
            var posts = await Connection.QueryAsync<PostRecord>(
                @"SELECT Post.* FROM Post
                    LEFT JOIN ""User"" u1 ON u1.Id = Post.UserId
                    WHERE u1.Username = @Username AND Post.WasDeleted = FALSE
                    ORDER BY Post.CreationDate DESC
                    LIMIT @Limit
                    OFFSET @Offset",
                BuildPaginationParams(new { Username = username }, pageNumber, pageSize)
            );

            //Get total count
            int totalCount = await Connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM Post LEFT JOIN ""User"" ON Post.UserId = ""User"".Id WHERE ""User"".Username = @Username AND Post.WasDeleted = FALSE",
                new { Username = username }
            );

            return new PagedResultSet<Post>(posts.Select(p => mapper.Map(p)), new PaginationInfo(pageNumber, pageSize, totalCount));

        }

        /// <summary>
        /// Find the newest posts by their creation date.
        /// </summary>
        /// <param name="space">The name of the space</param>
        /// <param name="pageNumber">Index of the page..</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>The result set.</returns>
        public async Task<PagedResultSet<Post>> FindBySpace(string space, int pageNumber, int pageSize) {
            var posts = await Connection.QueryAsync<PostRecord>(
                @"SELECT Post.* FROM Post
                    LEFT JOIN Space ON Space.Id = Post.SpaceId
                    WHERE LOWER(Space.Name) = LOWER(@Name) AND Post.WasDeleted = FALSE
                    ORDER BY Post.CreationDate DESC
                    LIMIT @Limit
                    OFFSET @Offset",
                BuildPaginationParams(new { Name = space }, pageNumber, pageSize)
            );

            //Get total count
            int totalCount = await Connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Post LEFT JOIN Space ON Post.SpaceId = Space.Id WHERE LOWER(Space.Name) = LOWER(@Name) AND Post.WasDeleted = FALSE;", new { Name = space }
            );

            return new PagedResultSet<Post>(posts.Select(p => mapper.Map(p)), new PaginationInfo(pageNumber, pageSize, totalCount));
        }

        /// <summary>
        /// Find the newest posts by their creation date.
        /// </summary>
        /// <param name="pageNumber">Index of the page..</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>The result set.</returns>
        public async Task<PagedResultSet<Post>> FindByNew(int pageNumber, int pageSize) {
            var posts = await Connection.QueryAsync<PostRecord>(
                @"SELECT Post.* FROM Post
                    WHERE Post.WasDeleted = FALSE
                    ORDER BY Post.CreationDate DESC
                    LIMIT @Limit
                    OFFSET @Offset",
                BuildPaginationParams(pageNumber, pageSize)
            );

            //Get total count
            int totalCount = await Connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Post LEFT JOIN Space ON Post.SpaceId = Space.Id WHERE Post.WasDeleted = FALSE;"
            );

            return new PagedResultSet<Post>(posts.Select(p => mapper.Map(p)), new PaginationInfo(pageNumber, pageSize, totalCount));
        }

        /// <summary>
        /// Find a post via it's unique ID.
        /// </summary>
        /// <param name="id">The ID of the post.</param>
        /// <returns>The post (if found).</returns>
        public async Task<Post?> FindById(int id) {
            var post = (await Connection.QueryAsync<PostRecord>(
                @"SELECT * FROM Post 
                    LEFT JOIN ""User"" u1 ON u1.Id = Post.UserId
                    LEFT JOIN Space ON Space.Id = Post.SpaceId
                    WHERE Post.Id = @Id AND Post.WasDeleted = FALSE;",
                new { Id = id }
            )).FirstOrDefault();

            return post != null ? mapper.Map(post) : null;
        }

        /// <summary>
        /// Add a post to the database.
        /// </summary>
        /// <param name="post">The post to add.</param>
        public async Task Add(Post post) {
            PostRecord rec = mapper.Reverse(post);

            post.Id = await Connection.QueryFirstOrDefaultAsync<int>(
                @"INSERT INTO Post 
                    (Title, Body, Type, CreationDate, UserId, SpaceId, WasUpdated, WasDeleted, CommentCount, Upvotes, Downvotes) 
                    VALUES 
                    (@Title, @Body, @Type, @CreationDate, @UserId, @SpaceId, @WasUpdated, @WasDeleted, @CommentCount, @Upvotes, @Downvotes) RETURNING Id;",
                rec
            );
        }

        /// <summary>
        /// Update a post in the database.
        /// </summary>
        /// <param name="post">The post to update.</param>
        public async Task Update(Post post) {
            await Connection.ExecuteAsync(
                @"UPDATE Post SET 
                    UserId = @UserId, 
                    Type = @Type, 
                    Title = @Title, 
                    Body = @Body, 
                    CreationDate = @CreationDate, 
                    WasUpdated = @WasUpdated, 
                    WasDeleted = @WasDeleted, 
                    CommentCount = @CommentCount,
                    Upvotes = @Upvotes,
                    Downvotes = @Downvotes
                    WHERE Id = @Id",
                mapper.Reverse(post)
            );

            post.WasUpdated = true;
        }

        /// <summary>
        /// Delete a post from the database.
        /// </summary>
        /// <param name="post">The post to delete.</param>
        public async Task Delete(Post post) {
            await Connection.ExecuteAsync(
                @"UPDATE Post SET WasDeleted = TRUE WHERE Id = @Id",
                mapper.Reverse(post)
            );

            post.WasDeleted = true;
        }
        #endregion
    }
}