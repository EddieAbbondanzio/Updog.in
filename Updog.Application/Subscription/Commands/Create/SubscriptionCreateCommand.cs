using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionCreateCommand : AuthenticatedCommand {
        #region Properties
        public SubscriptionCreateData Data { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionCreateCommand(SubscriptionCreateData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}