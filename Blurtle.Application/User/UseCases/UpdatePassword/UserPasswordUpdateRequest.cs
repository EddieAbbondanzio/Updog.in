using Blurtle.Domain;

namespace Blurtle.Application {
    public sealed class UserPasswordUpdateRequest {
        #region Properties
        public User User { get; }

        public string Password { get; }
        #endregion

        #region Constructor(s)
        public UserPasswordUpdateRequest(User user, string password) {
            User = user;
            Password = password;
        }
        #endregion
    }
}