namespace Blurtle.Application {
    public class LoginUserRequest {
        #region Properties
        public string Username { get; }

        public string Password { get; }
        #endregion

        #region Constructor(s)
        public LoginUserRequest(string username, string password) {
            Username = username;
            Password = password;
        }
        #endregion
    }
}