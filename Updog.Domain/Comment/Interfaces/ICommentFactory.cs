using Updog.Domain;

namespace Updog.Domain {
    public interface ICommentFactory {
        Comment Create(CommentCreate data, User user);
    }
}