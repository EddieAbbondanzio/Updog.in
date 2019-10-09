namespace Updog.Domain {
    public sealed class VoteOnPost : IValueObject {
        #region Properties
        public int PostId { get; }

        public VoteDirection VoteDirection { get; }
        #endregion

        #region Constructor(s)
        public VoteOnPost(int postId, VoteDirection voteDirection) {
            PostId = postId;
            VoteDirection = voteDirection;
        }
        #endregion
    }
}