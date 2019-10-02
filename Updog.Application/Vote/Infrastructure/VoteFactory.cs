using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteFactory : IVoteFactory {
        #region Publics
        public Vote ForComment(User user, int commentId, VoteDirection direction) => new Vote() {
            User = user,
            ResourceType = VotableEntityType.Comment,
            ResourceId = commentId,
            Direction = direction
        };

        public Vote ForPost(User user, int postId, VoteDirection direction) => new Vote() {
            User = user,
            ResourceType = VotableEntityType.Post,
            ResourceId = postId,
            Direction = direction
        };
        #endregion
    }
}