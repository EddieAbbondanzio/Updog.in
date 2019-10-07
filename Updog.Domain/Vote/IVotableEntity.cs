using System;

namespace Updog.Domain {
    /// <summary>
    /// A resource that can be voted on.
    /// </summary>
    public interface IVotableEntity {
        #region Properties
        /// <summary>
        /// The vote resource type it is.
        /// </summary>
        VotableEntityType VotableEntityType { get; }

        /// <summary>
        /// The upvotes / downvotes of the entity.
        /// </summary>
        VoteStats Votes { get; }
        #endregion
    }
}