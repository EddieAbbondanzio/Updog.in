using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to update a user's password.
    /// </summary>
    public sealed class UserUpdatePasswordCommand : AuthenticatedCommand {
        #region Properties
        public UserUpdatePasswordData Data { get; }
        #endregion

        #region Constructor(s)
        public UserUpdatePasswordCommand(UserUpdatePasswordData data, User user) : base(user) {
            Data = data;
        }
        #endregion
    }
}