namespace Updog.Domain {
    public class UserCredentials {
        #region Properties
        public string Username { get; }

        public string Password { get; }
        #endregion

        #region Constructor(s)
        public UserCredentials(string username, string password) {
            Username = username;
            Password = password;
        }
        #endregion
    }
}