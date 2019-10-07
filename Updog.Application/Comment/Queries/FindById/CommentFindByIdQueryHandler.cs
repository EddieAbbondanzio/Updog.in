using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFindByIdQueryHandler : QueryHandler<CommentFindByIdQuery> {
        #region Fields
        private ICommentReader commentReader;
        #endregion

        #region Constructor(s)
        public CommentFindByIdQueryHandler(ICommentReader commentReader) {
            this.commentReader = commentReader;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<CommentFindByIdQuery> context) {
            CommentReadView? comment = await commentReader.FindById(context.Input.CommentId, context.Input.User);

            if (comment == null) {
                context.Output.NotFound();
            } else {
                context.Output.Success(comment);
            }
        }
        #endregion
    }
}