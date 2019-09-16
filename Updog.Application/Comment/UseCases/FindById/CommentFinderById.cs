using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find a comment by it's ID.
    /// </summary>
    public sealed class CommentFinderById : IInteractor<FindByValueParams<int>, CommentView?> {
        #region Fields
        private IDatabase database;
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentFinderById(IDatabase database, ICommentViewMapper commentMapper) {
            this.database = database;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public async Task<CommentView?> Handle(FindByValueParams<int> input) {
            using (var connection = database.GetConnection()) {
                ICommentRepo commentRepo = database.GetRepo<ICommentRepo>(connection);

                Comment? c = await commentRepo.FindById(input.Value);

                if (c == null) {
                    return null;
                }

                if (input.User != null) {
                    IVoteRepo voteRepo = database.GetRepo<IVoteRepo>(connection);
                    await GetVotes(voteRepo, c, input.User);
                }

                return commentMapper.Map(c);
            }
        }

        /// <summary>
        /// Recursive helper to get the votes for all children.
        /// </summary>
        private async Task GetVotes(IVoteRepo voteRepo, Comment comment, User user) {
            comment.Vote = await voteRepo.FindByUserAndComment(user.Username, comment.Id);

            foreach (Comment child in comment.Children) {
                await GetVotes(voteRepo, child, user);
            }
        }
        #endregion

    }
}