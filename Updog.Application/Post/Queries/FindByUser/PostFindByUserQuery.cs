using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindByUserQuery : IQuery {
        #region Properties
        public string Username { get; }

        public User? User { get; }

        public Paging.PaginationInfo? Paging { get; }
        #endregion
        #region Constructor(s)
        public PostFindByUserQuery(string username, User? user = null, Paging.PaginationInfo? paging = null) {
            Username = username;
            User = user;
            Paging = paging;
        }
        #endregion
    }
}