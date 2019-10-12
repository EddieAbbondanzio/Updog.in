using Updog.Domain;

namespace Updog.Domain {
    public sealed class VoteFactory : IVoteFactory {
        #region Publics
        public Vote Create(int id, int userId, VotableEntityType entityType, int entityId, VoteDirection direction) => new Vote(id, userId, entityType, entityId, direction);
        public Vote CreateForComment(User user, int commentId, VoteDirection direction) => new Vote(user.Id, VotableEntityType.Comment, commentId, direction);

        public Vote CreateForPost(User user, int postId, VoteDirection direction) => new Vote(user.Id, VotableEntityType.Post, postId, direction);
        #endregion
    }
}