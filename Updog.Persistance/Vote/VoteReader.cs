using System.Threading.Tasks;
using Dapper;
using Updog.Domain;

namespace Updog.Persistance {
    public sealed class VoteReader : DatabaseReader<VoteReadView>, IVoteReader {
        #region Fields
        private IVoteReadViewMapper mapper;
        #endregion

        #region Constructor(s)
        public VoteReader(IDatabase database, IVoteReadViewMapper mapper) : base(database) {
            this.mapper = mapper;
        }
        #endregion

        #region Publics
        public async Task<VoteReadView?> FindByCommentAndUser(int commentId, int userId) {
            var vote = await Connection.QuerySingleOrDefaultAsync<VoteRecord>(
                @"SELECT * FROM Vote 
                    JOIN ""User"" u ON u.Id = Vote.UserId 
                    WHERE u.Id = @UserId AND Vote.ResourceType = @ResourceType AND Vote.ResourceId = @CommentId",
                new {
                    CommentId = commentId,
                    UserId = userId,
                    ResourceType = VotableEntityType.Comment
                }
            );

            return vote != null ? mapper.Map(vote) : null;
        }

        public async Task<VoteReadView?> FindByPostAndUser(int postId, int userId) {
            var vote = await Connection.QuerySingleOrDefaultAsync<VoteRecord>(
                @"SELECT * FROM Vote 
                    JOIN ""User"" u ON u.Id = Vote.UserId 
                    WHERE u.Id = @UserId AND Vote.ResourceType = @ResourceType AND Vote.ResourceId = @CommentId",
                new {
                    PostId = postId,
                    UserId = userId,
                    ResourceType = VotableEntityType.Post
                }
            );

            return vote != null ? mapper.Map(vote) : null;
        }
        #endregion
    }
}