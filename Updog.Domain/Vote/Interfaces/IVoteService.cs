using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IVoteService : IService<Vote> {
        Task<Vote> VoteOnPost(VoteOnPostData data, User user);

        Task<Vote> VoteOnComment(VoteOnCommentData data, User user);
    }
}