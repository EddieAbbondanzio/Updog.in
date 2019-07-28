namespace Updog.Application {
    public class LoginUserParams {
        #region Properties
        public string Username { get; }

        public string Password { get; }
        #endregion

        #region Constructor(s)
        public LoginUserParams(string username, string password) {
            Username = username;
            Password = password;
        }
        #endregion
    }
}