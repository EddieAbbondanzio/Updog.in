using Updog.Domain;
using Updog.Domain.Paging;

namespace Updog.Application {
    public sealed class PostFindByNewQuery : IQuery {
        #region Properties
        public User? User { get; }

        public PaginationInfo? Paging { get; }
        #endregion

        #region Constructor(s)
        public PostFindByNewQuery(User? user = null, PaginationInfo? paging = null) {
            User = user;
            Paging = paging;
        }
        #endregion
    }
}