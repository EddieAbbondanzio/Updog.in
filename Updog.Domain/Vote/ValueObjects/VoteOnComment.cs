namespace Updog.Domain {
    public sealed class VoteOnComment : IValueObject {
        #region Properties
        public int CommentId { get; }

        public VoteDirection VoteDirection { get; }
        #endregion

        #region Constructor(s)
        public VoteOnComment(int commentId, VoteDirection voteDirection) {
            CommentId = commentId;
            VoteDirection = voteDirection;
        }
        #endregion
    }
}