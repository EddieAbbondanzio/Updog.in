using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// A view of a vote to send back to a client.
    /// </summary>
    public sealed class VoteView : IView {
        #region Properties
        /// <summary>
        /// The type of resource being voted on.
        /// </summary>
        /// <value></value>
        public VotableEntityType ResourceType { get; }

        /// <summary>
        /// The ID of the resource.
        /// </summary>
        public int ResourceId { get; }

        /// <summary>
        /// The direction of the vote.
        /// </summary>
        /// <value></value>
        public VoteDirection Direction { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new vote view.
        /// </summary>
        /// <param name="resourceType">If its a post or comment.</param>
        /// <param name="resourceId">The ID of the resource.</param>
        /// <param name="direction">The direction of the vote.</param>
        public VoteView(VotableEntityType resourceType, int resourceId, VoteDirection direction) {
            ResourceType = resourceType;
            ResourceId = resourceId;
            Direction = direction;
        }
        #endregion
    }
}