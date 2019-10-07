using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindByNewQueryHandler : QueryHandler<PostFindByNewQuery> {
        #region Fields
        private IPostReader postReader;
        #endregion

        #region Constructor(s)
        public PostFindByNewQueryHandler(IPostReader postReader) {
            this.postReader = postReader;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<PostFindByNewQuery> context) {
            PagedResultSet<PostReadView> posts = await postReader.FindByNew(context.Input.Paging, context.Input.User);
            context.Output.Success(posts);
        }
        #endregion
    }
}