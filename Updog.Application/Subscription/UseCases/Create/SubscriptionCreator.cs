using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to creat e new subscription for a space.
    /// </summary>
    public sealed class SubscriptionCreator : IInteractor<SubscriptionCreateParams, SubscriptionView> {
        #region Fields
        private IDatabase database;
        private ISubscriptionViewMapper subscriptionMapper;
        #endregion

        #region Constructor(s)
        public SubscriptionCreator(IDatabase database, ISubscriptionViewMapper subscriptionMapper) {
            this.database = database;
            this.subscriptionMapper = subscriptionMapper;
        }
        #endregion

        #region Publics
        public async Task<SubscriptionView> Handle(SubscriptionCreateParams input) {
            using (var connection = database.GetConnection()) {
                ISpaceRepo spaceRepo = database.GetRepo<ISpaceRepo>(connection);
                ISubscriptionRepo subRepo = database.GetRepo<ISubscriptionRepo>(connection);

                //Check to see if the Space exists first
                Space? space = await spaceRepo.FindByName(input.Space);

                if (space == null) {
                    throw new InvalidOperationException($"No space with name {input.Space} exists.");
                }

                //Ensure no sub for this combo is already in place
                Subscription? existingSub = await subRepo.FindByUserAndSpace(input.User.Username, input.Space);

                if (existingSub != null) {
                    return subscriptionMapper.Map(existingSub);
                }

                //Create the subscription
                Subscription sub = new Subscription() {
                    Space = space,
                    User = input.User
                };

                await subRepo.Add(sub);

                //Update sub count
                space.SubscriptionCount++;

                await spaceRepo.Update(space);

                return subscriptionMapper.Map(sub);
            }

        }
        #endregion
    }
}