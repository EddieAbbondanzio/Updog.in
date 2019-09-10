using Updog.Domain;

namespace Updog.Application {
    public sealed class UpdateUserParams : IAuthenticatedActionParams {
        /* This was done to prepare for later on when a user has more
         * info that they can set. Violates YAGNI but whatever.
         */

        #region Properties
        public User User { get; }

        public string Email { get; }
        #endregion

        #region Constructor(s)
        public UpdateUserParams(User user, string email) {
            User = user;
            Email = email;
        }
        #endregion
    }
}