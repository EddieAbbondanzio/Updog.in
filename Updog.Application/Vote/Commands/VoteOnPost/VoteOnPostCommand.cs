using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnPostCommand : AuthenticatedCommand {
        #region Properties
        public VoteOnPost Data { get; }
        #endregion

        #region Constructor(s)
        public VoteOnPostCommand(VoteOnPost data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}