using Updog.Domain;

namespace Updog.Application {
    public sealed class UserUpdateCommand : UserAlterCommand {
        #region Properties
        public UserUpdate Update { get; }
        #endregion

        #region Constructor(s)
        public UserUpdateCommand(string username, UserUpdate data, User user) : base(username, user) {
            Update = data;
        }
        #endregion
    }
}