using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to creat e new subscription for a space.
    /// </summary>
    public sealed class SubscriptionCreator : IInteractor<SubscriptionCreateParams, SubscriptionView> {
        #region Fields
        private ISpaceRepo _spaceRepo;

        private ISubscriptionRepo _subscriptionRepo;

        private ISubscriptionViewMapper _subscriptionMapper;
        #endregion

        #region Constructor(s)
        public SubscriptionCreator(ISpaceRepo spaceRepo, ISubscriptionRepo subscriptionRepo, ISubscriptionViewMapper subscriptionMapper) {
            _spaceRepo = spaceRepo;
            _subscriptionRepo = subscriptionRepo;
            _subscriptionMapper = subscriptionMapper;
        }
        #endregion

        #region Publics
        public async Task<SubscriptionView> Handle(SubscriptionCreateParams input) {
            //Check to see if the Space exists first
            Space? s = await _spaceRepo.FindByName(input.Space);

            if (s == null) {
                throw new NotFoundException($"No space with name {input.Space} exists.");
            }

            //Ensure no sub for this combo is already in place
            Subscription? existingSub = await _subscriptionRepo.FindByUserAndSpace(input.User.Username, input.Space);

            if (existingSub != null) {
                return _subscriptionMapper.Map(existingSub);
            }

            //Create the subscription
            Subscription sub = new Subscription() {
                Space = s,
                User = input.User
            };

            await _subscriptionRepo.Add(sub);
            return _subscriptionMapper.Map(sub);
        }
        #endregion
    }
}