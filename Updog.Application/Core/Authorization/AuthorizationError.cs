using Updog.Domain;

namespace Updog.Application {
    public sealed class AuthorizationError : Error {
        #region Properties
        public AuthorizationError(PolicyResult result) : base(result.Failure ?? "Unauthorized") { }
        #endregion
    }
}