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
        public async Task<Vote> VoteOnComment(VoteOnCommentData data, User user) {
            Vote? oldVote = await repo.FindByUserAndComment(user.Username, data.CommentId);

            if (oldVote != null) {
                await repo.Delete(oldVote);
            }

            Vote newVote = factory.CreateForComment(user, data.CommentId, data.VoteDirection);
            await repo.Add(newVote);
            await bus.Dispatch(new VoteOnCommentEvent(newVote));

            return newVote;
        }

        public async Task<Vote> VoteOnPost(VoteOnPostData data, User user) {
            Vote? oldVote = await repo.FindByUserAndPost(user.Username, data.PostId);

            if (oldVote != null) {
                await repo.Delete(oldVote);
            }

            Vote newVote = factory.CreateForPost(user, data.PostId, data.VoteDirection);
            await repo.Add(newVote);
            await bus.Dispatch(new VoteOnPostEvent(newVote));

            return newVote;
        }
        #endregion
    }
}