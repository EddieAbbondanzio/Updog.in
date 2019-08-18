namespace Updog.Application {
    public class UserLoginParams {
        #region Properties
        public string Username { get; }

        public string Password { get; }
        #endregion

        #region Constructor(s)
        public UserLoginParams(string username, string password) {
            Username = username;
            Password = password;
        }
        #endregion
    }
}