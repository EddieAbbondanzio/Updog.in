using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindByIdQueryHandler : QueryHandler<PostFindByIdQuery, PostReadView?> {
        #region Fields
        private IPostReader postReader;
        #endregion

        #region Constructor(s)
        public PostFindByIdQueryHandler(IPostReader postReader) {
            this.postReader = postReader;
        }
        #endregion

        #region Publics
        protected async override Task<PostReadView?> ExecuteQuery(PostFindByIdQuery query) => await postReader.FindById(query.PostId);
        #endregion
    }
}