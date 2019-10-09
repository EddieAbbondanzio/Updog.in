using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionCreateCommand : AuthenticatedCommand {
        #region Properties
        public SubscriptionCreate Data { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionCreateCommand(SubscriptionCreate data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}