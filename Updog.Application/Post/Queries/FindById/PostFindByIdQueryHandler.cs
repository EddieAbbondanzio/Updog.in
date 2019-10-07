using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindByIdQueryHandler : QueryHandler<PostFindByIdQuery> {
        #region Fields
        private IPostReader postReader;
        #endregion

        #region Constructor(s)
        public PostFindByIdQueryHandler(IPostReader postReader) {
            this.postReader = postReader;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<PostFindByIdQuery> context) {
            PostReadView? post = await postReader.FindById(context.Input.PostId);

            if (post != null) {
                context.Output.Success(post);
            } else {
                context.Output.NotFound();
            }
        }
        #endregion
    }
}