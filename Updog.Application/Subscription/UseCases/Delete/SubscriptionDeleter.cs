using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to cancel a user's subscription with a space.
    /// </summary>
    public sealed class SubscriptionDeleter : IInteractor<SubscriptionDeleteParams> {
        #region Fields
        private IDatabase database;
        private ISubscriptionViewMapper subscriptionMapper;
        #endregion

        #region Constructor(s)
        public SubscriptionDeleter(IDatabase database, ISubscriptionViewMapper subscriptionMapper) {
            this.database = database;
            this.subscriptionMapper = subscriptionMapper;
        }
        #endregion

        #region Publics
        public async Task Handle(SubscriptionDeleteParams input) {
            using (var conenction = database.GetConnection()) {
                ISpaceRepo spaceRepo = database.GetRepo<ISpaceRepo>(conenction);
                ISubscriptionRepo subRepo = database.GetRepo<ISubscriptionRepo>(conenction);

                //Pull in the space first
                Space? space = await spaceRepo.FindByName(input.Space);

                if (space == null) {
                    throw new InvalidOperationException($"No space with name {input.Space} exists.");
                }

                //Try to pull in the subscription
                Subscription? sub = await subRepo.FindByUserAndSpace(input.User.Username, input.Space);

                if (sub == null) {
                    throw new InvalidOperationException("Subscription does not exist");
                }

                await subRepo.Delete(sub);

                space.SubscriptionCount--;

                await spaceRepo.Update(space);
            }
        }
        #endregion
    }
}