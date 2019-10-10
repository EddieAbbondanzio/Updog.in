using Updog.Domain;

namespace Updog.Application {
    public sealed class UserUpdateCommand : AuthenticatedCommand {
        #region Properties
        public UserUpdate Update { get; }
        #endregion

        #region Constructor(s)
        public UserUpdateCommand(UserUpdate data, User user) : base(user) {
            Update = data;
        }
        #endregion
    }
}