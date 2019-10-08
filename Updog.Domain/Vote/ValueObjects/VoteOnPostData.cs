namespace Updog.Domain {
    public sealed class VoteOnPostData : IValueObject {
        #region Properties
        public int PostId { get; }

        public VoteDirection VoteDirection { get; }
        #endregion

        #region Constructor(s)
        public VoteOnPostData(int postId, VoteDirection voteDirection) {
            PostId = postId;
            VoteDirection = voteDirection;
        }
        #endregion
    }
}