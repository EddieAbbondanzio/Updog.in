namespace Updog.Persistance {
    /// <summary>
    /// Data transfer object for subscriptions.
    /// </summary>
    public sealed class SubscriptionRecord {
        #region Properties
        /// <summary>
        /// Primary key of the subscription.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key of the user it belongs to.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Foreign key of the space it's for.
        /// </summary>
        /// <value></value>
        public int SpaceId { get; set; }
        #endregion
    }
}