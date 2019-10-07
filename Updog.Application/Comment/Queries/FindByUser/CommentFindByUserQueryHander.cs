using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFindByUserQueryHandler : QueryHandler<CommentFindByUserQuery> {
        #region Fields
        private ICommentReader commentReader;
        #endregion

        #region Constructor(s)
        public CommentFindByUserQueryHandler(ICommentReader commentReader) {
            this.commentReader = commentReader;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<CommentFindByUserQuery> context) {
            PagedResultSet<CommentReadView> comments = await commentReader.FindByUser(context.Input.Username, context.Input.Paging, context.Input.User);
            context.Output.Success(comments);
        }
        #endregion
    }
}