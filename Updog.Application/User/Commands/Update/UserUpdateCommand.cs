using Updog.Domain;

namespace Updog.Application {
    public sealed class UserUpdateCommand : ICommand {
        #region Properties
        public User User { get; }

        public string Email { get; }
        #endregion

        #region Constructor(s)
        public UserUpdateCommand(User user, string email) {
            User = user;
            Email = email;
        }
        #endregion
    }
}