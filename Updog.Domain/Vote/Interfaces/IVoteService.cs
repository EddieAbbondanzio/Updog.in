using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IVoteService : IService<Vote> {
        Task<Vote> VoteOnPost(VoteOnPost data, User user);

        Task<Vote> VoteOnComment(VoteOnComment data, User user);
    }
}