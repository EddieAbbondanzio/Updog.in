using Updog.Domain;

namespace Updog.Application {
    public abstract class SubscriptionAlterCommand : AuthenticatedCommand {
        #region Properties
        public string Space { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionAlterCommand(string space, User user) : base(user) {
            Space = space;
        }
        #endregion
    }
}