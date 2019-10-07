using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IVoteReader : IReader<VoteReadView> {
        #region Publics
        Task<VoteReadView?> FindByPostAndUser(int postId, int userId);
        Task<VoteReadView?> FindByCommentAndUser(int commentId, int userId);
        #endregion
    }
}