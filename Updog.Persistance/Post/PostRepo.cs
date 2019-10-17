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
        private IPostFactory factory;
        #endregion

        #region Constructor(s)
        public PostRepo(IDatabase database, IPostFactory factory) : base(database) {
            this.factory = factory;
        }
        #endregion

        #region Publics
        public override async Task<Post?> FindById(int id) => (await Connection.QueryAsync<PostRecord>(
            @"SELECT * FROM Post 
                LEFT JOIN ""User"" u1 ON u1.Id = Post.UserId
                LEFT JOIN Space ON Space.Id = Post.SpaceId
                WHERE Post.Id = @Id AND Post.WasDeleted = FALSE;",
            new { Id = id }
        )).Select(p => Map(p)).FirstOrDefault();

        public override async Task Add(Post post) => post.Id = await Connection.QueryFirstOrDefaultAsync<int>(
            @"INSERT INTO Post 
                (Title, Body, Type, CreationDate, UserId, SpaceId, WasUpdated, WasDeleted, CommentCount, Upvotes, Downvotes) 
                VALUES 
                (@Title, @Body, @Type, @CreationDate, @UserId, @SpaceId, @WasUpdated, @WasDeleted, @CommentCount, @Upvotes, @Downvotes) RETURNING Id;",
            Reverse(post)
        );

        public override async Task Update(Post post) => await Connection.ExecuteAsync(
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
            Reverse(post)
        );

        public override async Task Delete(Post post) => await Connection.ExecuteAsync(
            @"UPDATE Post SET WasDeleted = TRUE WHERE Id = @Id",
            Reverse(post)
        );

        #endregion

        #region Privates
        private Post Map(PostRecord rec) => factory.Create(rec.Id, rec.UserId, rec.SpaceId, rec.Type, rec.Title, rec.Body, rec.CreationDate, rec.CommentCount, rec.Upvotes, rec.Downvotes, rec.WasUpdated, rec.WasDeleted);

        private PostRecord Reverse(Post post) => new PostRecord() {
            Id = post.Id,
            UserId = post.UserId,
            SpaceId = post.SpaceId,
            Type = post.Type,
            Title = post.Title,
            Body = post.Body,
            CreationDate = post.CreationDate,
            CommentCount = post.CommentCount,
            Upvotes = post.Votes.Upvotes,
            Downvotes = post.Votes.Downvotes,
            WasUpdated = post.WasUpdated,
            WasDeleted = post.WasDeleted
        };
        #endregion
    }
}