using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindBySpaceQueryHandler : QueryHandler<PostFindBySpaceQuery> {
        #region Fields
        private IPostReader postReader;
        #endregion

        #region Constructor(s)
        public PostFindBySpaceQueryHandler(IPostReader postReader) {
            this.postReader = postReader;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<PostFindBySpaceQuery> context) {
            PagedResultSet<PostReadView> posts = await postReader.FindBySpace(context.Input.Space, context.Input.Paging, context.Input.User);
            context.Output.Success(posts);
        }
        #endregion
    }
}