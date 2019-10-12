namespace Updog.Domain {
    /// <summary>
    /// A subscription for a sub space.
    /// </summary>
    public sealed class Subscription : Entity<Subscription>, IUserEntity {
        #region Properties
        /// <summary>
        /// The space the subscription is for.
        /// </summary>
        public int SpaceId { get; set; }

        /// <summary>
        /// The user it belongs to.
        /// </summary>
        public int UserId { get; set; }
        #endregion

        #region Constructor(s)
        internal Subscription(int id, int userId, int spaceId) {
            Id = id;
            UserId = userId;
            SpaceId = spaceId;
        }

        internal Subscription(int userId, int spaceId) {
            SpaceId = spaceId;
            UserId = userId;
        }
        #endregion
    }
}