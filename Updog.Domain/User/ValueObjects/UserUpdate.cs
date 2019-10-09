namespace Updog.Domain {
    public sealed class UserUpdate : IValueObject {
        #region Properties
        public string Email { get; }
        #endregion

        #region Constructor(s)
        public UserUpdate(string email) {
            Email = email;
        }
        #endregion
    }
}