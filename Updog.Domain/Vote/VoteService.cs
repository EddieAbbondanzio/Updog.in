using System.Threading.Tasks;

namespace Updog.Domain {
    public sealed class VoteService : IVoteService {
        #region Fields
        private IEventBus bus;
        private IVoteFactory factory;
        private IVoteRepo repo;
        #endregion

        #region Constructor(s)
        public VoteService(IEventBus bus, IVoteFactory factory, IVoteRepo repo) {
            this.bus = bus;
            this.factory = factory;
            this.repo = repo;
        }
        #endregion

        #region Publics
        public async Task<Vote> VoteOnComment(VoteOnComment data, User user) {
            Vote? oldVote = await repo.FindByUserAndComment(user.Username, data.CommentId);

            if (oldVote != null) {
                await repo.Delete(oldVote);
            }

            Vote newVote = factory.CreateForComment(user, data.CommentId, data.VoteDirection);
            await repo.Add(newVote);
            await bus.Dispatch(new VoteOnCommentEvent(data.CommentId, newVote, oldVote));

            return newVote;
        }

        public async Task<Vote> VoteOnPost(VoteOnPost data, User user) {
            Vote? oldVote = await repo.FindByUserAndPost(user.Username, data.PostId);

            if (oldVote != null) {
                await repo.Delete(oldVote);
            }

            Vote newVote = factory.CreateForPost(user, data.PostId, data.VoteDirection);
            await repo.Add(newVote);
            await bus.Dispatch(new VoteOnPostEvent(data.PostId, newVote, oldVote));

            return newVote;
        }
        #endregion
    }
}