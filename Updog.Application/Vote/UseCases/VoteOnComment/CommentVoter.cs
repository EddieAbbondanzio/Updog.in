using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to vote on a comment.
    /// </summary>
    public sealed class CommentVoter : IInteractor<VoteOnCommentParams, Vote> {
        #region Publics
        public async Task<Vote> Handle(VoteOnCommentParams input) {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}