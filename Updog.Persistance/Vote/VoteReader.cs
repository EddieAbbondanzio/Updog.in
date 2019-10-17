using System.Threading.Tasks;
using Dapper;
using Updog.Domain;

namespace Updog.Persistance {
    public sealed class VoteReader : DatabaseReader<VoteReadView>, IVoteReader {
        #region Constructor(s)
        public VoteReader(IDatabase database) : base(database) { }
        #endregion

        #region Publics
        public async Task<VoteReadView?> FindByCommentAndUser(int commentId, int userId) {
            var vote = await Connection.QuerySingleOrDefaultAsync<VoteRecord>(
                @"SELECT * FROM vote 
                    JOIN ""user"" u ON u.id = vote.user_id 
                    WHERE u.id = @UserId AND vote.resource_type = @ResourceType AND vote.resource_id = @CommentId",
                new {
                    CommentId = commentId,
                    UserId = userId,
                    ResourceType = VotableEntityType.Comment
                }
            );

            return vote != null ? Map(vote) : null;
        }

        public async Task<VoteReadView?> FindByPostAndUser(int postId, int userId) {
            var vote = await Connection.QuerySingleOrDefaultAsync<VoteRecord>(
                @"SELECT * FROM vote 
                    JOIN ""user"" u ON u.id = vote.user_id 
                    WHERE u.id = @UserId AND vote.resource_type = @ResourceType AND vote.resource_id = @CommentId",
                new {
                    PostId = postId,
                    UserId = userId,
                    ResourceType = VotableEntityType.Post
                }
            );

            return vote != null ? Map(vote) : null;
        }
        #endregion

        #region Privates
        private VoteReadView Map(VoteRecord source) => new VoteReadView() { Direction = source.Direction };
        #endregion
    }
}