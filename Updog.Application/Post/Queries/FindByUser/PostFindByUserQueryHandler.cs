using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindByUserQueryHandler : QueryHandler<PostFindByUserQuery> {
        #region Fields
        private IPostReader postReader;
        #endregion

        #region Constructor(s)
        public PostFindByUserQueryHandler(IPostReader postReader) {
            this.postReader = postReader;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<PostFindByUserQuery> context) {
            PagedResultSet<PostReadView> posts = await postReader.FindByUser(context.Input.Username, context.Input.Paging, context.Input.User);
            context.Output.Success(posts);
        }
        #endregion
    }
}