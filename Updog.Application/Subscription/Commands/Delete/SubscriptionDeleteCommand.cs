using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionDeleteCommand : AuthenticatedCommand {
        #region Properties
        public string Space { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionDeleteCommand(string space, User user) : base(user) {
            Space = space;
        }
        #endregion
    }
}