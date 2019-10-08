using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFindByIdQueryHandler : QueryHandler<CommentFindByIdQuery, CommentReadView?> {
        #region Fields
        private ICommentReader commentReader;
        #endregion

        #region Constructor(s)
        public CommentFindByIdQueryHandler(ICommentReader commentReader) {
            this.commentReader = commentReader;
        }
        #endregion

        #region Publics
        protected async override Task<CommentReadView?> ExecuteQuery(CommentFindByIdQuery query) => await commentReader.FindById(query.CommentId, query.User);
        #endregion
    }
}