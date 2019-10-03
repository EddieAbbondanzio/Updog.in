using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindByNewQuery : IQuery {
        #region Properties
        public User? User { get; }

        public Paging.PaginationInfo Paging { get; }
        #endregion

        #region Constructor(s)
        public PostFindByNewQuery(User? user = null, Paging.PaginationInfo? paging = null) {
            User = user;
            Paging = paging;
        }
        #endregion
    }
}