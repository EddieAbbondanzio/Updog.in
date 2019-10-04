using Updog.Domain;

namespace Updog.Domain {
    public interface ICommentFactory {
        Comment Create(CommentCreationData data, User user);
    }
}