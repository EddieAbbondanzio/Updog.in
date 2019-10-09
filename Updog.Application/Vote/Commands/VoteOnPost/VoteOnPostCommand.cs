using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnPostCommand : AuthenticatedCommand {
        #region Properties
        public VoteOnPostData Data { get; }
        #endregion

        #region Constructor(s)
        public VoteOnPostCommand(VoteOnPostData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}