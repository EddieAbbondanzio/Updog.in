using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindBySpaceQueryHandler : QueryHandler<PostFindBySpaceQuery, PagedResultSet<PostReadView>> {
        #region Fields
        private IPostReader postReader;
        #endregion

        #region Constructor(s)
        public PostFindBySpaceQueryHandler(IPostReader postReader) {
            this.postReader = postReader;
        }
        #endregion

        #region Publics
        protected async override Task<Either<PagedResultSet<PostReadView>, Error>> ExecuteQuery(PostFindBySpaceQuery query) => await postReader.FindBySpace(query.Space, query.Paging, query.User);
        #endregion
    }
}