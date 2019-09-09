using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find comments on a post.
    /// </summary>
    public sealed class CommentFinderByUser : IInteractor<CommentFinderByUserParams, PagedResultSet<CommentView>> {
        #region Fields
        private IDatabase database;
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentFinderByUser(IDatabase database, ICommentViewMapper commentMapper) {
            this.database = database;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public async Task<PagedResultSet<CommentView>> Handle(CommentFinderByUserParams input) {
            using (var connection = database.GetConnection()) {
                ICommentRepo commentRepo = database.GetRepo<ICommentRepo>(connection);

                PagedResultSet<Comment> comments = await commentRepo.FindByUser(input.Username, input.PageNumber, input.PageSize);

                return new PagedResultSet<CommentView>(
                    comments.Select(c => commentMapper.Map(c)),
                    comments.Pagination
                );
            }
        }
        #endregion
    }
}