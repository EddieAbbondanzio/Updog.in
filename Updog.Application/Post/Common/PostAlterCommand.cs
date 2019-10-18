using Updog.Domain;

namespace Updog.Application {
    public abstract class PostAlterCommand : AuthenticatedCommand {
        #region Properties
        public int PostId { get; }
        #endregion

        #region Constructor(s)
        public PostAlterCommand(int postId, User user) : base(user) {
            PostId = postId;
        }
        #endregion
    }
}