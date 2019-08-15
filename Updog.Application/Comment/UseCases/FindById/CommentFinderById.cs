using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find a comment by it's ID.
    /// </summary>
    public sealed class CommentFinderById : IInteractor<int, CommentInfo> {
        #region Fields
        private ICommentRepo commentRepo;

        private IUserRepo userRepo;
        #endregion

        #region Constructor(s)
        public CommentFinderById(ICommentRepo commentRepo, IUserRepo userRepo) {
            this.commentRepo = commentRepo;
            this.userRepo = userRepo;
        }
        #endregion

        #region Publics
        public async Task<CommentInfo> Handle(int input) {
            Comment comment = await commentRepo.FindById(input);
            User author = await this.userRepo.FindById(comment.UserId);

            return new CommentInfo(comment.Id, author.Username, comment.Body);
        }
        #endregion
    }
}