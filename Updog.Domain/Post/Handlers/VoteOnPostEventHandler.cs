using System;
using System.Threading.Tasks;

namespace Updog.Domain {
    public sealed class VoteOnPostEventHandler : IDomainEventHandler<VoteOnPostEvent> {
        #region Fields
        private IPostRepo repo;
        #endregion  

        #region Constructor(s)
        public VoteOnPostEventHandler(IPostRepo repo) {
            this.repo = repo;
        }
        #endregion

        #region Publics
        public async Task Handle(VoteOnPostEvent domainEvent) {
            Post? p = await repo.FindById(domainEvent.PostId);

            if (p == null) {
                throw new InvalidOperationException();
            }

            if (domainEvent.OldVote != null) {
                p.Votes.RemoveVote(domainEvent.OldVote.Direction);
            }

            p.Votes.AddVote(domainEvent.NewVote.Direction);
            await repo.Update(p);
        }
        #endregion
    }
}