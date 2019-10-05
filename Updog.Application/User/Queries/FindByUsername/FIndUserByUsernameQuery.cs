using Updog.Domain;

namespace Updog.Application {
    public sealed class FindUserByUsernameQuery : AnonymousQuery {
        #region Properties
        public string Username { get; set; } = "";
        #endregion
    }
}