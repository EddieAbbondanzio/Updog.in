using System;
using System.Threading.Tasks;

namespace Updog.Domain {
    public sealed class SubscriptionService : ISubscriptionService {
        #region Fields
        IEventBus bus;
        ISubscriptionFactory factory;
        ISpaceRepo spaceRepo;
        ISubscriptionRepo repo;
        #endregion

        #region Constructor(s)
        public SubscriptionService(IEventBus bus, ISubscriptionFactory factory, ISpaceRepo spaceRepo, ISubscriptionRepo repo) {
            this.bus = bus;
            this.factory = factory;
            this.spaceRepo = spaceRepo;
            this.repo = repo;
        }
        #endregion

        #region Publics
        public async Task<Subscription> CreateSubscription(SubscriptionCreate create, User user) {
            Space? space = await spaceRepo.FindByName(create.Space);

            if (space == null) {
                throw new NotFoundException($"No space with name {create.Space} found.");
            }

            Subscription s = factory.CreateFor(user, space);
            await repo.Add(s);
            await bus.Dispatch(new SubscriptionCreateEvent(s));

            return s;
        }

        public async Task DeleteSubscription(string space, User user) {
            Subscription? s = await repo.FindByUserAndSpace(user.Username, space);

            if (s == null) {
                throw new NotFoundException($"No subscription for {space} found.");
            }

            await repo.Delete(s);
            await bus.Dispatch(new SubscriptionDeleteEvent(s));
        }
        #endregion
    }
}