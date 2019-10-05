using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFindByIdQuery : AnonymousQuery {
        #region Properties
        public int CommentId { get; set; }
        #endregion
    }
}