namespace Updog.Domain {
    public sealed class PostDeleteData : IValueObject {
        #region Properties
        public int PostId { get; }
        #endregion

        #region Constructor(s)
        public PostDeleteData(int postId) {
            PostId = postId;
        }
        #endregion
    }
}