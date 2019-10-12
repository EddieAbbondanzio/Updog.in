namespace Updog.Domain {
    /// <summary>
    /// A vote entity on a post, or comment.
    /// </summary>
    public sealed class Vote : Entity<Vote>, IUserEntity {
        #region Properties
        public int UserId { get; set; }
        public VotableEntityType ResourceType { get; set; }
        public int ResourceId { get; set; }
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
    }
}