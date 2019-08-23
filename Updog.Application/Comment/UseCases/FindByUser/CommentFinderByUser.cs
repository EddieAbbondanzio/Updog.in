using System;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find comments on a post.
    /// </summary>
    public sealed class CommentFinderByUser : IInteractor<CommentFinderByUserParams, CommentView[]> {
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
        public async Task<CommentView[]> Handle(CommentFinderByUserParams input) {
            Comment[] comments = await commentRepo.FindByUser(input.Username, input.PaginationInfo);
            CommentView[] views = new CommentView[comments.Length];

            for (int i = 0; i < comments.Length; i++) {
                views[i] = commentMapper.Map(comments[i]);
            }

            return views;
        }
        #endregion
    }
}