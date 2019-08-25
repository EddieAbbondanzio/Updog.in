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
        /// <summary>
        /// The underlying repo for finding comments in the database.
        /// </summary>
        private ICommentRepo commentRepo;

        /// <summary>
        /// Mapper to convert a comment into it's DTO.
        /// </summary>
        private IMapper<Comment, CommentView> commentMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment finder by post.
        /// </summary>
        /// <param name="commentRepo">The CRUD interface for comments.</param>
        /// <param name="commentMapper">DTO mapper.</param>
        public CommentFinderByUser(ICommentRepo commentRepo, IMapper<Comment, CommentView> commentMapper) {
            this.commentRepo = commentRepo;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public async Task<PagedResultSet<CommentView>> Handle(CommentFinderByUserParams input) {
            PagedResultSet<Comment> comments = await commentRepo.FindByUser(input.Username, input.PageNumber, input.PageSize);
            List<CommentView> views = new List<CommentView>();

            foreach (Comment c in comments) {
                views.Add(commentMapper.Map(c));
            }

            return new PagedResultSet<CommentView>(views, comments.Pagination);
        }
        #endregion
    }
}