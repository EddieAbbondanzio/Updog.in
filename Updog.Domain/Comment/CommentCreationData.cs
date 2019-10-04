namespace Updog.Domain {
    public sealed class CommentCreationData : IValueObject {
        #region Properties
        /// <summary>
        /// The ID of the post it belongs to.
        /// </summary>
        public int PostId { get; }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The ID of the parent comment.
        /// </summary>
        public int ParentId { get; }
        #endregion

        #region Constructor(s)
        public CommentCreationData(int postId, string body, int parentId = 0) {
            PostId = postId;
            Body = body;
            ParentId = parentId;
        }
        #endregion

    }
}