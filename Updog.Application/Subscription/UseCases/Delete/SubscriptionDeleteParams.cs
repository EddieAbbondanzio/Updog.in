using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to delete a new subscription for a space.
    /// </summary>
    public sealed class SubscriptionDeleteParams {
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
        public SubscriptionDeleteParams(string space, User user) {
            Space = space;
            User = user;
        }
        #endregion
    }
}