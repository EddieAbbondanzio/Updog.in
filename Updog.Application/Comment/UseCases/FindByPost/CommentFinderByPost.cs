using System.Threading.Tasks;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find comments on a post.
    /// </summary>
    public sealed class CommentFinderByPost : IInteractor<int, CommentInfo[]> {
        #region Fields
        /// <summary>
        /// The underlying repo for finding comments in the database.
        /// </summary>
        private ICommentRepo commentRepo;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment finder by post.
        /// </summary>
        /// <param name="commentRepo">The CRUD interface for comments.</param>
        public CommentFinderByPost(ICommentRepo commentRepo) {
            this.commentRepo = commentRepo;
        }
        #endregion

        #region Publics
        public async Task<CommentInfo[]> Handle(int input) => await commentRepo.FindCommentsByPost(input);
        #endregion
    }
}