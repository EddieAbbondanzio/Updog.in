using Updog.Domain;

namespace Updog.Domain {
    public interface ICommentFactory {
        Comment Create(CommentCreateData data, User user);
    }
}