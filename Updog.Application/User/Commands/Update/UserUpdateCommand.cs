using Updog.Domain;

namespace Updog.Application {
    public sealed class UserUpdateCommand : AuthenticatedCommand {
        #region Properties
        public UserUpdateData Data { get; }
        #endregion

        #region Constructor(s)
        public UserUpdateCommand(UserUpdateData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}