using System;

namespace Updog.Domain {
    /// <summary>
    /// A resource that can be voted. on.
    /// </summary>
    public abstract class VotableEntity : IUserEntity {
        #region Properties
        /// <summary>
        /// The unique ID of the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user it belongs to.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The vote resource type it is.
        /// </summary>
        public abstract VotableEntityType EntityType { get; }

        /// <summary>
        /// How many upvotes the resource has recieved.
        /// </summary>
        public int Upvotes { get; set; }

        /// <summary>
        /// How many downvotes the resource has recieved.
        /// </summary>
        public int Downvotes { get; set; }
        #endregion

        #region Publics
        /// <summary>
        /// Add a new vote to the counts.
        /// </summary>
        /// <param name="vote">The type of vote to add.</param>
        public void AddVote(VoteDirection vote) {
            switch (vote) {
                case VoteDirection.Up:
                    Upvotes++;
                    break;
                case VoteDirection.Down:
                    Downvotes++;
                    break;
            }
        }

        /// <summary>
        /// Remove a vote from the couns.
        /// </summary>
        /// <param name="vote">The vote to remove.</param>
        public void RemoveVote(VoteDirection vote) {
            switch (vote) {
                case VoteDirection.Up:
                    Upvotes--;
                    break;
                case VoteDirection.Down:
                    Downvotes--;
                    break;
            }
        }
        #endregion
    }
}