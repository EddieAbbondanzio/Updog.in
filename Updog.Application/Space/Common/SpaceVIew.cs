using System;

namespace Updog.Application {
    /// <summary>
    /// Business entity of a space.
    /// </summary>
    public sealed class SpaceView : IView {
        #region Properties
        /// <summary>
        /// The unique ID of the view.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The unique name of the space.
        /// </summary>
        /// <value></value>
        public string Name { get; }

        /// <summary>
        /// The text description explaining the space.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The number of subscribers it has.
        /// </summary>
        public int SubscriptionCount { get; }

        /// <summary>
        /// When it was created.
        /// </summary>
        public DateTime CreationDate { get; }

        /// <summary>
        /// The user that created it.
        /// </summary>
        public UserView User { get; }
        #endregion

        #region Constructor(s)
        public SpaceView(int id, string name, string description, int subscriptionCount, DateTime creationDate, UserView user) {
            Id = id;
            Name = name;
            Description = description;
            SubscriptionCount = subscriptionCount;
            CreationDate = creationDate;
            User = user;
        }
        #endregion
    }
}