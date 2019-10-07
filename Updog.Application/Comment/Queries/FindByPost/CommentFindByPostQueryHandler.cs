using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFindByPostQueryHandler : QueryHandler<CommentFindByPostQuery> {
        #region Fields
        private ICommentReader commentReader;
        #endregion

        #region Constructor(s)
        public CommentFindByPostQueryHandler(ICommentReader commentReader) {
            this.commentReader = commentReader;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<CommentFindByPostQuery> context) {
            IEnumerable<CommentReadView> comments = await commentReader.FindByPost(context.Input.PostId, context.Input.User);
            context.Output.Success(comments);
        }
        #endregion
    }
}