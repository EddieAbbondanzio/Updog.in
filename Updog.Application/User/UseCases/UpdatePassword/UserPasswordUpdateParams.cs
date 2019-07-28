using Blurtle.Domain;

namespace Blurtle.Application {
    public sealed class UserPasswordUpdateParams {
        #region Properties
        public User User { get; }

        public string Password { get; }
        #endregion

        #region Constructor(s)
        public UserPasswordUpdateParams(User user, string password) {
            User = user;
            Password = password;
        }
        #endregion
    }
}