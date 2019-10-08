namespace Updog.Domain {
    public sealed class CommentDeleteData : IValueObject {
        #region Properties
        public int CommentId { get; }
        #endregion

        #region Constructor(s)
        public CommentDeleteData(int commentId) {
            CommentId = commentId;
        }
        #endregion
    }
}