using Updog.Domain;

namespace Updog.Application {
    public sealed class IsUsernameAvailableQuery : AnonymousQuery {
        #region Properties
        public string Username { get; set; } = "";
        #endregion
    }
}