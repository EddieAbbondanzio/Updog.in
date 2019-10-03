using Updog.Domain;

namespace Updog.Application {
    public sealed class SubscriptionDeleteCommand : ICommand {
        #region Properties
        /// <summary>
        /// The name of the space.
        /// </summary>
        /// <value></value>
        public string Space { get; }

        /// <summary>
        /// The user performing the action.
        /// </summary>
        public User User { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionDeleteCommand(string space, User user) {
            Space = space;
            User = user;
        }
        #endregion
    }
}