using Updog.Domain;

namespace Updog.Application {
    public interface ICommentFactory {
        Comment Create(CommentCreationData data, User user);
    }
}