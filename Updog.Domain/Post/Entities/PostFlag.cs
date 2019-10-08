namespace Updog.Domain {
    /// <summary>
    /// A flag to mark the type of content in the post.
    /// </summary>
    public sealed class PostFlag : IEntity {
        #region Properties
        /// <summary>
        /// The unique ID of the flag.
        /// </summary>
        /// <value></value>
        public int Id { get; set; }

        /// <summary>
        /// The post it belongs to.
        /// </summary>
        /// <value></value>
        public int PostId { get; set; }

        /// <summary>
        /// The type of flag it is.
        /// </summary>
        /// <value></value>
        public PostFlagType Type { get; set; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post flag.
        /// </summary>
        /// <param name="post">The post it belongs to.</param>
        /// <param name="type">The type of flag it is.</param>
        public PostFlag(Post post, PostFlagType type) {
            PostId = post.Id;
            Type = type;
        }
        #endregion
    }
}