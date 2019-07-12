using Blurtle.Domain;

namespace Blurtle.Application {
    public sealed class UpdateUserRequest {
        #region Properties
        public User User { get; }

        public string Email { get; }
        #endregion

        #region Constructor(s)
        public UpdateUserRequest(User user, string email) {
            User = user;
            Email = email;
        }
        #endregion
    }
}