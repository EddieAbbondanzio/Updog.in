namespace Updog.Domain {
    public sealed class PostUpdateData : IValueObject {
        #region Properties
        public int PostId { get; }

        public string Body { get; }
        #endregion

        #region Constructor(s)
        public PostUpdateData(int postId, string body) {
            PostId = postId;
            Body = body;
        }
        #endregion
    }
}