namespace Updog.Domain {
    public sealed class VoteOnCommentData : IValueObject {
        #region Properties
        public int CommentId { get; }

        public VoteDirection VoteDirection { get; }
        #endregion

        #region Constructor(s)
        public VoteOnCommentData(int commentId, VoteDirection voteDirection) {
            CommentId = commentId;
            VoteDirection = voteDirection;
        }
        #endregion
    }
}