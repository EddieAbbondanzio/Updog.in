using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Updog.Application;
using Updog.Domain;
using System.Linq;

namespace Updog.Persistance {
    /// <summary>
    /// CRUD interface for managing votes in the database.
    /// </summary>
    public sealed class VoteRepo : DatabaseRepo<Vote>, IVoteRepo {
        private IVoteFactory factory;

        #region Constructor(s)
        public VoteRepo(IDatabase database, IVoteFactory factory) : base(database) {
            this.factory = factory;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a vote by it's unique ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        /// <returns>The matching vote found.</returns>
        public override async Task<Vote?> FindById(int id) => (await Connection.QueryAsync<VoteRecord>(
            @"SELECT * FROM Vote 
                JOIN ""User"" u ON u.Id = Vote.UserId 
                WHERE Vote.Id = @Id",
            new { Id = id }
            )).Select(v => Map(v)).FirstOrDefault();

        /// <summary>
        /// Find a vote via its comment and the user that casted it.
        /// </summary>
        /// <param name="username">The username of the user to look for.</param>
        /// <param name="commentId">The ID of the comment.</param>
        /// <returns>The vote found (if any).</returns>
        public async Task<Vote?> FindByUserAndComment(string username, int commentId) => (await Connection.QueryAsync<VoteRecord>(
                @"SELECT * FROM Vote 
                    JOIN ""User"" u ON u.Id = Vote.UserId 
                    WHERE u.Username = @Username AND Vote.ResourceType = @ResourceType AND Vote.ResourceId = @CommentId",
                new { Username = username, CommentId = commentId, ResourceType = VotableEntityType.Comment }
            )).Select(v => Map(v)).FirstOrDefault();

        /// <summary>
        /// Find a vote via it's post and the user that casted it.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <param name="postId">The post ID></param>
        /// <returns>The vote found (if any).</returns>
        public async Task<Vote?> FindByUserAndPost(string username, int postId) => (await Connection.QueryAsync<VoteRecord>(
                @"SELECT * FROM Vote 
                    JOIN ""User"" U ON U.Id = Vote.UserId 
                    WHERE u.Username = @Username AND Vote.ResourceType = @ResourceType AND Vote.ResourceId = @PostId",
                new { Username = username, PostId = postId, ResourceType = VotableEntityType.Post }
            )).Select(v => Map(v)).FirstOrDefault();

        /// <summary>
        /// Add a new vote to the database.
        /// </summary>
        /// <param name="vote">The vote to add.</param>
        public override async Task Add(Vote vote) => vote.Id = await Connection.QueryFirstOrDefaultAsync<int>(
                @"INSERT INTO Vote 
                    (UserId, ResourceId, ResourceType, Direction) 
                    VALUES (@UserId, @ResourceId, @ResourceType, @Direction) RETURNING Id;",
                    Reverse(vote));

        /// <summary>
        /// Update an existing vote with the database.
        /// </summary>
        /// <param name="vote">The vote to update.</param>
        public override async Task Update(Vote vote) => await Connection.ExecuteAsync(
                @"UPDATE Vote SET 
                    UserId = @UserId, 
                    ResourceId = @ResourceId, 
                    ResourceType = @ResourceType, 
                    Direction = @Direction
                    WHERE Vote.Id = @Id",
                    Reverse(vote)
            );

        /// <summary>
        /// Delete an existing vote with the database.
        /// </summary>
        /// <param name="vote">The vote to delete.</param>
        public override async Task Delete(Vote vote) => await Connection.ExecuteAsync(
                @"DELETE FROM Vote WHERE Vote.ID = @Id",
                Reverse(vote)
            );
        #endregion

        #region Privates
        private Vote Map(VoteRecord rec) => factory.Create(rec.Id, rec.UserId, rec.ResourceType, rec.ResourceId, rec.Direction);

        private VoteRecord Reverse(Vote v) => new VoteRecord() { Id = v.Id, UserId = v.UserId, ResourceType = v.ResourceType, ResourceId = v.ResourceId, Direction = v.Direction };
        #endregion
    }
}