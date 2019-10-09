using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionDeleteCommand : AuthenticatedCommand {
        #region Properties
        public SubscriptionDeleteData Data { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionDeleteCommand(SubscriptionDeleteData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}