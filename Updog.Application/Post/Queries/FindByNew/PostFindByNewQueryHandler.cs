using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindByNewQueryHandler : QueryHandler<PostFindByNewQuery, PagedResultSet<PostReadView>> {
        #region Fields
        private IPostReader postReader;
        #endregion

        #region Constructor(s)
        public PostFindByNewQueryHandler(IPostReader postReader) {
            this.postReader = postReader;
        }
        #endregion

        #region Publics
        protected async override Task<Either<PagedResultSet<PostReadView>, Error>> ExecuteQuery(PostFindByNewQuery query) => await postReader.FindByNew(query.Paging, query.User);
        #endregion
    }
}