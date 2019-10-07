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
    public sealed class PostRepo : DatabaseRepo<Post>, IPostRepo {
        #region Fields
        private IPostMapper mapper;
        #endregion

        #region Constructor(s)
        public PostRepo(IDatabase database, IPostMapper mapper) : base(database) {
            this.mapper = mapper;
        }
        #endregion

        #region Publics
        public override async Task<Post?> FindById(int id) {
            var post = (await Connection.QueryAsync<PostRecord>(
                @"SELECT * FROM Post 
                    LEFT JOIN ""User"" u1 ON u1.Id = Post.UserId
                    LEFT JOIN Space ON Space.Id = Post.SpaceId
                    WHERE Post.Id = @Id AND Post.WasDeleted = FALSE;",
                new { Id = id }
            )).FirstOrDefault();

            return post != null ? mapper.Map(post) : null;
        }

        public override async Task Add(Post post) {
            PostRecord rec = mapper.Reverse(post);

            post.Id = await Connection.QueryFirstOrDefaultAsync<int>(
                @"INSERT INTO Post 
                    (Title, Body, Type, CreationDate, UserId, SpaceId, WasUpdated, WasDeleted, CommentCount, Upvotes, Downvotes) 
                    VALUES 
                    (@Title, @Body, @Type, @CreationDate, @UserId, @SpaceId, @WasUpdated, @WasDeleted, @CommentCount, @Upvotes, @Downvotes) RETURNING Id;",
                rec
            );
        }

        public override async Task Update(Post post) {
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
        }

        public override async Task Delete(Post post) {
            await Connection.ExecuteAsync(
                @"UPDATE Post SET WasDeleted = TRUE WHERE Id = @Id",
                mapper.Reverse(post)
            );
        }
        #endregion
    }
}