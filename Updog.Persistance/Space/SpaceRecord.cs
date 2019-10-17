using System;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// A database entity for the post table.
    /// </summary>
    internal sealed class SpaceRecord {
        #region Properties
        /// <summary>
        /// Primary key of the space.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique human readable name.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Human readable description.
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// When the space was founded.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// How many subscribers it has.
        /// </summary>
        public int SubscriptionCount { get; set; }

        /// <summary>
        /// Foreign key of the user that created it.
        /// </summary>
        /// <value></value>
        public int UserId { get; set; }

        /// <summary>
        /// if the space is considered a default space.
        /// </summary>
        /// <value></value>
        public bool IsDefault { get; set; }
        #endregion
    }
}