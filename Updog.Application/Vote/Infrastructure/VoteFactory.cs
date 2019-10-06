using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteFactory : IVoteFactory {
        #region Publics
        public Vote CreateForComment(User user, int commentId, VoteDirection direction) => new Vote() {
            UserId = user.Id,
            ResourceType = VotableEntityType.Comment,
            ResourceId = commentId,
            Direction = direction
        };

        public Vote CreateForPost(User user, int postId, VoteDirection direction) => new Vote() {
            UserId = user.Id,
            ResourceType = VotableEntityType.Post,
            ResourceId = postId,
            Direction = direction
        };
        #endregion
    }
}