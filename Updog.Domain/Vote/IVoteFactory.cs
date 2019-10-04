using Updog.Domain;

namespace Updog.Domain {
    public interface IVoteFactory : IFactory<Vote> {
        Vote ForComment(User user, int commentId, VoteDirection direction);

        Vote ForPost(User user, int postId, VoteDirection direction);
    }
}