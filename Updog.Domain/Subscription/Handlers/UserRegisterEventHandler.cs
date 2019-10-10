using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Updog.Domain {
    public sealed class UserRegisterEventHandler : IDomainEventHandler<UserRegisterEvent> {
        #region Fields
        private ISpaceRepo spaceRepo;
        private ISubscriptionRepo subscriptionRepo;
        private ISubscriptionFactory subscriptionFactory;
        #endregion

        #region Constructor(s)
        public UserRegisterEventHandler(ISpaceRepo spaceRepo, ISubscriptionRepo subscriptionRepo, ISubscriptionFactory subscriptionFactory) {
            this.spaceRepo = spaceRepo;
            this.subscriptionRepo = subscriptionRepo;
            this.subscriptionFactory = subscriptionFactory;
        }
        #endregion

        #region Publics
        public async Task Handle(UserRegisterEvent domainEvent) {
            IEnumerable<Space> defaultSpaces = await spaceRepo.FindDefault();

            IEnumerable<Subscription> subs = defaultSpaces.Select(s => subscriptionFactory.CreateFor(domainEvent.User, s));

            foreach (Subscription s in subs) {
                await subscriptionRepo.Add(s);
            }
        }
        #endregion
    }
}