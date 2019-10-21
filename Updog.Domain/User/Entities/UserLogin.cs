using Updog.Domain;

namespace Updog.Domain {
    public sealed class UserLogin : Entity<UserLogin> {
        #region Properties
        public int UserId { get; }
        #endregion

        #region Constructor(s)
        internal UserLogin(int userId) {
            UserId = userId;
        }

        internal UserLogin(int id, int userId) {
            Id = id;
            UserId = userId;
        }
        #endregion
    }
}