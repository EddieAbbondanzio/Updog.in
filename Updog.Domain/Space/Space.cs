using System;

namespace Updog.Domain {
    /// <summary>
    /// Subforum for collecting similar themed posts.
    /// </summary>
    public sealed class Space : IEntity {
        #region Constants
        /// <summary>
        /// The maximum number of characters allowed in a space name.
        /// </summary>
        public const int NameMaxLength = 24;

        /// <summary>
        /// The maximum number of characters allowed in a space description.
        /// </summary>
        public const int DescriptionMaxLength = 512;
        #endregion

        #region Properties
        /// <summary>
        /// Unique ID of the space.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }

        /// <summary>
        /// The name of the subspace.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date and time the space was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// The user that created the space.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The number of subscribers the space has.
        /// </summary>
        public int SubscriptionCount { get; set; }

        /// <summary>
        /// If the space is considered a default space for all new users
        /// to subscribe to.
        /// </summary>
        public bool IsDefault { get; set; }

        public string Description { get; set; }
        #endregion

        #region Publics
        /// <summary>
        /// Check to see if another object matches the current space.
        /// </summary>
        /// <param name="obj">The other object to check.</param>
        /// <returns>True if the space matches the object.</returns>
        public override bool Equals(object obj) {
            Space s = obj as Space;

            if (s == null) {
                return false;
            }

            return Equals(s);
        }

        /// <summary>
        /// Check to see if the space matches another space.
        /// </summary>
        /// <param name="space">The other space to check.</param>
        /// <returns>True if the spaces match.</returns>
        public bool Equals(Space space) => Id == space.Id;

        /// <summary>
        /// Get a unique hashcode of the object.
        /// </summary>
        /// <returns>The unique hashcode.</returns>
        public override int GetHashCode() => Id;
        #endregion
    }
}