
namespace Updog.Application {
    public sealed class AuthenticationError : Error {
        #region Constructor(s)
        public AuthenticationError(string message = "Authentication failed") : base(message) { }
        #endregion
    }
}