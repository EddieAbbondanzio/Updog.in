using System;
using System.Threading.Tasks;

namespace Updog.Domain {
    public sealed class VoteOnCommentEventHandler : IDomainEventHandler<VoteOnCommentEvent> {
        #region Fields
        private ICommentRepo repo;
        #endregion  

        #region Constructor(s)
        public VoteOnCommentEventHandler(ICommentRepo repo) {
            this.repo = repo;
        }
        #endregion

        #region Publics
        public async Task Handle(VoteOnCommentEvent domainEvent) {
            Comment? p = await repo.FindById(domainEvent.CommentId);

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