using Updog.Domain;

namespace Updog.Application {
    public abstract class AuthenticatedCommand : ICommand {
        #region Properties
        public User User { get; }
        #endregion

        #region Constructor(s)
        public AuthenticatedCommand(User user) {
            User = user;
        }
        #endregion
    }
}