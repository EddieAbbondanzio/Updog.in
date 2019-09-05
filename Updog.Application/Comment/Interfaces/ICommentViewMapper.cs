using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a comment entity to it's view and back.
    /// </summary>
    public interface ICommentViewMapper : IMapper<Comment, CommentView> { }
}