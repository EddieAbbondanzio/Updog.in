namespace Updog.Application {
    /// <summary>
    /// View of a subscription to a space.
    /// </summary>
    public sealed class SubscriptionView {
        #region Properties
        /// <summary>
        /// The ID of the subscription.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The user it belongs to.
        /// </summary>
        public UserView User { get; }

        /// <summary>
        /// The space the subscription is for.
        /// </summary>
        /// <value></value>
        public SpaceView Space { get; }
        #endregion

        #region Constructor(s)
        public SubscriptionView(int id, UserView user, SpaceView space) {
            Id = id;
            User = user;
            Space = space;
        }
        #endregion
    }
}