using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFindByPostQuery : AnonymousQuery {
        #region Properties
        public int PostId { get; set; }
        #endregion
    }
}