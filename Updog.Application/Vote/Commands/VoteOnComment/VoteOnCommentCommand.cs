using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnCommentCommand : AuthenticatedCommand {
        #region Properties
        public VoteOnComment Data { get; }
        #endregion

        #region Constructor(s)
        public VoteOnCommentCommand(VoteOnComment data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}