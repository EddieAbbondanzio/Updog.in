using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to cancel a user's subscription with a space.
    /// </summary>
    public sealed class SubscriptionDeleter : IInteractor<SubscriptionDeleteParams> {
        #region Fields
        private ISpaceRepo _spaceRepo;

        private ISubscriptionRepo _subscriptionRepo;

        private ISubscriptionViewMapper _subscriptionMapper;
        #endregion

        #region Constructor(s)
        public SubscriptionDeleter(ISpaceRepo spaceRepo, ISubscriptionRepo subscriptionRepo, ISubscriptionViewMapper subscriptionMapper) {
            _spaceRepo = spaceRepo;
            _subscriptionRepo = subscriptionRepo;
            _subscriptionMapper = subscriptionMapper;
        }
        #endregion

        #region Publics
        public async Task Handle(SubscriptionDeleteParams input) {
            //Pull in the space first
            Space? s = await _spaceRepo.FindByName(input.Space);

            if (s == null) {
                throw new NotFoundException($"No space with name {input.Space} exists.");
            }

            //Try to pull in the subscription
            Subscription? sub = await _subscriptionRepo.FindByUserAndSpace(input.User.Username, input.Space);

            if (sub == null) {
                throw new InvalidOperationException("Subscription does not exist");
            }

            await _subscriptionRepo.Delete(sub);
        }
        #endregion
    }
}