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
            @"SELECT * FROM post 
                LEFT JOIN ""user"" u1 ON u1.id = post.user_id
                LEFT JOIN space ON space.id = post.space_id
                WHERE post.id = @Id AND post.was_deleted = FALSE;",
            new { Id = id }
        )).Select(p => Map(p)).FirstOrDefault();

        public override async Task Add(Post post) => post.Id = await Connection.QueryFirstOrDefaultAsync<int>(
            @"INSERT INTO post 
                (title, body, type, creation_date, user_id, space_id, was_updated, was_deleted, comment_count, upvotes, downvotes) 
                VALUES 
                (@Title, @Body, @Type, @CreationDate, @UserId, @SpaceId, @WasUpdated, @WasDeleted, @CommentCount, @Upvotes, @Downvotes) RETURNING Id;",
            Reverse(post)
        );

        public override async Task Update(Post post) => await Connection.ExecuteAsync(
            @"UPDATE Post SET 
                user_id = @UserId, 
                type = @Type, 
                title = @Title, 
                body = @Body, 
                creation_date = @CreationDate, 
                was_updated = @WasUpdated, 
                was_deleted = @WasDeleted, 
                comment_count = @CommentCount,
                upvotes = @Upvotes,
                downvotes = @Downvotes
                WHERE id = @Id",
            Reverse(post)
        );

        public override async Task Delete(Post post) => await Connection.ExecuteAsync(
            @"UPDATE post SET was_deleted = TRUE WHERE id = @Id",
            Reverse(post)
        );


        public async Task<bool> IsOwner(int postId, string username) {
            var owner = await Connection.ExecuteScalarAsync<string>(
                @"SELECT u.username FROM post p 
                    JOIN ""user"" u ON u.id = p.user_id 
                    WHERE p.id = @Id",
                new { Id = postId });
            return owner == username;
        }
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