using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnCommentCommand : AuthenticatedCommand {
        #region Properties
        public VoteOnCommentData Data { get; }
        #endregion

        #region Constructor(s)
        public VoteOnCommentCommand(VoteOnCommentData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}