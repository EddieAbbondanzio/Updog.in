using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionFindByUserParams : IAuthenticatedActionParams {
        #region Properties
        public User User { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionFindByUserParams(User user) {
            User = user;
        }
        #endregion
    }
}