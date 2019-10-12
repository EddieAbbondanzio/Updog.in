namespace Updog.Domain {
    /// <summary>
    /// A vote entity on a post, or comment.
    /// </summary>
    public sealed class Vote : IEntity, IUserEntity {
        #region Properties
        /// <summary>
        /// The unqiue ID of the vote.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// THe user it belongs to.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The type of resource that was voted on.
        /// </summary>
        public VotableEntityType ResourceType { get; set; }

        /// <summary>
        /// The ID of the resource it belongs to.
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// The direction of the vote.
        /// </summary>
        public VoteDirection Direction { get; set; }
        #endregion

        #region Constructor(s)
        internal Vote(int id, int userId, VotableEntityType resourceType, int resourceId, VoteDirection direction) {
            Id = id;
            UserId = userId;
            ResourceType = resourceType;
            ResourceId = resourceId;
            Direction = direction;
        }

        internal Vote(int userId, VotableEntityType resourceType, int resourceId, VoteDirection direction) {
            UserId = userId;
            ResourceType = resourceType;
            ResourceId = resourceId;
            Direction = direction;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Check to see if another object matches the current Vote.
        /// </summary>
        /// <param name="obj">The other object to check.</param>
        /// <returns>True if the Vote matches the object.</returns>
        public override bool Equals(object obj) {
            Vote? u = obj as Vote;

            if (u == null) {
                return false;
            }

            return Equals(u);
        }

        /// <summary>
        /// Check to see if the vote matches another voe.
        /// </summary>
        /// <param name="vote">The other vote to check.</param>
        /// <returns>True if the vote match.</returns>
        public bool Equals(Vote vote) => Id == vote.Id;

        /// <summary>
        /// Get a unique hashcode of the object.
        /// </summary>
        /// <returns>The unique hashcode.</returns>
        public override int GetHashCode() => Id;
        #endregion
    }
}