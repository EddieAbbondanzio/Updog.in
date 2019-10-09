using System;
using System.Threading.Tasks;

namespace Updog.Domain {
    public sealed class SubscriptionDeleteEventHandler : IDomainEventHandler<SubscriptionDeleteEvent> {
        #region Fields
        private ISpaceRepo repo;
        #endregion

        #region Constructor(s)
        public SubscriptionDeleteEventHandler(ISpaceRepo repo) {
            this.repo = repo;
        }
        #endregion

        public async Task Handle(SubscriptionDeleteEvent domainEvent) {
            Space? s = await repo.FindById(domainEvent.Subscription.SpaceId);

            if (s == null) {
                throw new InvalidOperationException();
            }

            s.SuscriberCount--;
            await repo.Update(s);
        }
    }
}