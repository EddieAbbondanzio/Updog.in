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
    public sealed class VoteRepo : DapperRepo<Vote>, IVoteRepo {
        private IVoteRecordMapper voteMapper;

        #region Constructor(s)
        public VoteRepo(DbConnection connection) : base(connection) {
            voteMapper = new VoteRecordMapper(new UserRecordMapper());
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a vote by it's unique ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        /// <returns>The matching vote found.</returns>
        public async Task<Vote?> FindById(int id) {
            return (await Connection.QueryAsync<VoteRecord, UserRecord, Vote>(
                @"SELECT * FROM Vote 
                    JOIN ""User"" u ON u.Id = Vote.UserId 
                    WHERE Vote.Id = @Id",
                (VoteRecord voteRec, UserRecord userRec) => voteMapper.Map(Tuple.Create(voteRec, userRec)),
                new { Id = id }
            )).FirstOrDefault();
        }

        /// <summary>
        /// Find a vote via its comment and the user that casted it.
        /// </summary>
        /// <param name="username">The username of the user to look for.</param>
        /// <param name="commentId">The ID of the comment.</param>
        /// <returns>The vote found (if any).</returns>
        public async Task<Vote?> FindByUserAndComment(string username, int commentId) {
            return (await Connection.QueryAsync<VoteRecord, UserRecord, Vote>(
                @"SELECT * FROM Vote 
                    JOIN ""User"" u ON u.Id = Vote.UserId 
                    WHERE u.Username = @Username AND Vote.ResourceType = @ResourceType AND Vote.ResourceId = @CommentId",
                (VoteRecord voteRec, UserRecord userRec) => voteMapper.Map(Tuple.Create(voteRec, userRec)),
                new { Username = username, CommentId = commentId, ResourceType = VotableEntityType.Comment }
            )).FirstOrDefault();
        }

        /// <summary>
        /// Find a vote via it's post and the user that casted it.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <param name="postId">The post ID></param>
        /// <returns>The vote found (if any).</returns>
        public async Task<Vote?> FindByUserAndPost(string username, int postId) {
            return (await Connection.QueryAsync<VoteRecord, UserRecord, Vote>(
                @"SELECT * FROM Vote 
                    JOIN ""User"" U ON U.Id = Vote.UserId 
                    WHERE u.Username = @Username AND Vote.ResourceType = @ResourceType AND Vote.ResourceId = @PostId",
                (VoteRecord voteRec, UserRecord userRec) => voteMapper.Map(Tuple.Create(voteRec, userRec)),
                new { Username = username, PostId = postId, ResourceType = VotableEntityType.Post }
            )).FirstOrDefault();
        }

        /// <summary>
        /// Add a new vote to the database.
        /// </summary>
        /// <param name="vote">The vote to add.</param>
        public async Task Add(Vote vote) {
            vote.Id = await Connection.QueryFirstOrDefaultAsync<int>(
                @"INSERT INTO Vote 
                    (UserId, ResourceId, ResourceType, Direction) 
                    VALUES (@UserId, @ResourceId, @ResourceType, @Direction) RETURNING Id;",
                    voteMapper.Reverse(vote).Item1);
        }

        /// <summary>
        /// Update an existing vote with the database.
        /// </summary>
        /// <param name="vote">The vote to update.</param>
        public async Task Update(Vote vote) {
            await Connection.ExecuteAsync(
                @"UPDATE Vote SET 
                    UserId = @UserId, 
                    ResourceId = @ResourceId, 
                    ResourceType = @ResourceType, 
                    Direction = @Direction
                    WHERE Vote.Id = @Id",
                    voteMapper.Reverse(vote).Item1
            );
        }

        /// <summary>
        /// Delete an existing vote with the database.
        /// </summary>
        /// <param name="vote">The vote to delete.</param>
        public async Task Delete(Vote vote) {
            await Connection.ExecuteAsync(
                @"DELETE FROM Vote WHERE Vote.ID = @Id",
                voteMapper.Reverse(vote).Item1
            );
        }
        #endregion
    }
}