namespace Updog.Domain {
    public sealed class UserUpdateData : IValueObject {
        #region Properties
        public string Email { get; }
        #endregion

        #region Constructor(s)
        public UserUpdateData(string email) {
            Email = email;
        }
        #endregion
    }
}