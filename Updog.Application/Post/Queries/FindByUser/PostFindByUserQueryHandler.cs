using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindByUserQueryHandler : QueryHandler<PostFindByUserQuery, PagedResultSet<PostReadView>> {
        #region Fields
        private IPostReader postReader;
        #endregion

        #region Constructor(s)
        public PostFindByUserQueryHandler(IPostReader postReader) {
            this.postReader = postReader;
        }
        #endregion

        #region Publics
        protected async override Task<PagedResultSet<PostReadView>> ExecuteQuery(PostFindByUserQuery query) => await postReader.FindByUser(query.Username, query.Paging, query.User);
        #endregion
    }
}