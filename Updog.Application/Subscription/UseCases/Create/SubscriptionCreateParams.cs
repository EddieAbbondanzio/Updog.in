using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to create a new subscription for a space.
    /// </summary>
    public sealed class SubscriptionCreateParams {
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
        public SubscriptionCreateParams(string space, User user) {
            Space = space;
            User = user;
        }
        #endregion
    }
}