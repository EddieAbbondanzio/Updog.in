using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFindByPostQueryHandler : QueryHandler<CommentFindByPostQuery, IEnumerable<CommentReadView>> {
        #region Fields
        private ICommentReader commentReader;
        #endregion

        #region Constructor(s)
        public CommentFindByPostQueryHandler(ICommentReader commentReader) {
            this.commentReader = commentReader;
        }
        #endregion

        #region Publics
        protected async override Task<IEnumerable<CommentReadView>> ExecuteQuery(CommentFindByPostQuery query) => await commentReader.FindByPost(query.PostId, query.User);
        #endregion
    }
}