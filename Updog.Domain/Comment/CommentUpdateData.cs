namespace Updog.Domain {
    public sealed class CommentUpdateData : IValueObject {
        #region Properties
        public int CommentId { get; set; }

        public string Body { get; }
        #endregion

        #region Constructor(s)
        public CommentUpdateData(int commentId, string body) {
            CommentId = commentId;
            Body = body;
        }
        #endregion
    }
}