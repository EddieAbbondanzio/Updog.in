using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFindByUserQueryHandler : QueryHandler<CommentFindByUserQuery, PagedResultSet<CommentReadView>> {
        #region Fields
        private ICommentReader commentReader;
        #endregion

        #region Constructor(s)
        public CommentFindByUserQueryHandler(ICommentReader commentReader) {
            this.commentReader = commentReader;
        }
        #endregion

        #region Publics
        protected async override Task<PagedResultSet<CommentReadView>> ExecuteQuery(CommentFindByUserQuery query) => await commentReader.FindByUser(query.Username, query.Paging, query.User);
        #endregion
    }
}