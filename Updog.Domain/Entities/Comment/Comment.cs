namespace Updog.Domain {
    /// <summary>
    /// A comment associated with a post.
    /// </summary>
    public sealed class Comment : IEntity {
        #region Constants
        /// <summary>
        /// The maximum number of characters in a comment body.
        /// </summary>
        public const int BodyMaxLength = 10_000;
        #endregion

        #region Properties
        /// <summary>
        /// The ID of the comment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The ID of the user that made the comment.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The parent post the comment belongs to.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// The parent comment ID (if any).
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; set; }

        public bool WasUpdated { get; set; }

        public bool WasDeleted { get; set; }
        #endregion

    }
}