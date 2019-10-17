using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Record in the vote table.
    /// </summary>
    internal sealed class VoteRecord {
        #region Properties
        /// <summary>
        /// Unique ID of the vote.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }

        /// <summary>
        /// The user that cast it.
        /// </summary>
        /// <value></value>
        public int UserId { get; set; }

        /// <summary>
        /// The ID of the resource (Comment, or Post).
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// The resource it's voting on.
        /// </summary>
        public VotableEntityType ResourceType { get; set; }

        /// <summary>
        /// The type of vote it is.
        /// </summary>
        public VoteDirection Direction { get; set; }
        #endregion
    }
}