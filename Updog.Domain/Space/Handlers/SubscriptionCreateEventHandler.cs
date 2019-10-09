using System;
using System.Threading.Tasks;

namespace Updog.Domain {
    public sealed class SubscriptionCreateEventHandler : IDomainEventHandler<SubscriptionCreateEvent> {
        #region Fields
        private ISpaceRepo repo;
        #endregion

        #region Constructor(s)
        public SubscriptionCreateEventHandler(ISpaceRepo repo) {
            this.repo = repo;
        }
        #endregion

        public async Task Handle(SubscriptionCreateEvent domainEvent) {
            Space? s = await repo.FindById(domainEvent.Subscription.SpaceId);

            if (s == null) {
                throw new InvalidOperationException();
            }

            s.SuscriberCount++;
            await repo.Update(s);
        }
    }
}