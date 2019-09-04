namespace Updog.Domain {
    /// <summary>
    /// A subscription for a sub space.
    /// </summary>
    public sealed class Subscription : IEntity {
        #region Properties
        /// <summary>
        /// The unique ID of the subscription.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The space the subscription is for.
        /// </summary>
        public Space Space { get; set; }

        /// <summary>
        /// The user it belongs to.
        /// </summary>
        public User User { get; set; }
        #endregion

        #region Publics
        /// <summary>
        /// Check to see if another object matches the current subscription.
        /// </summary>
        /// <param name="obj">The other object to check.</param>
        /// <returns>True if the user matches the object.</returns>
        public override bool Equals(object obj) {
            Subscription s = obj as Subscription;

            if (s == null) {
                return false;
            }

            return Equals(s);
        }

        /// <summary>
        /// Check to see if the subscription matches another subscription.
        /// </summary>
        /// <param name="sub">The other subscription to check.</param>
        /// <returns>True if the user match.</returns>
        public bool Equals(Subscription sub) => Id == sub.Id;

        /// <summary>
        /// Get a unique hashcode of the object.
        /// </summary>
        /// <returns>The unique hashcode.</returns>
        public override int GetHashCode() => Id;
        #endregion
    }
}