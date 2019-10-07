using Updog.Domain;

namespace Updog.Domain {
    public interface IVoteFactory : IFactory<Vote> {
        Vote CreateForComment(User user, int commentId, VoteDirection direction);

        Vote CreateForPost(User user, int postId, VoteDirection direction);
    }
}