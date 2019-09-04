using System;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// A database entity for the post table.
    /// </summary>
    public sealed class SpaceRecord {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int SubscriptionCount { get; set; }
        public int UserId { get; set; }

        public bool IsDefault { get; set; }
        #endregion
    }
}