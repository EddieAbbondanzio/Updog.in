using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionDeleteCommand : AuthenticatedCommand {
        #region Properties
        public SubscriptionDelete Data { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionDeleteCommand(SubscriptionDelete data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}