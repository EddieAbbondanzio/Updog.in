using Updog.Domain;

namespace Updog.Application {
    public sealed class NotFoundError : Error {
        #region Constructor(s)
        public NotFoundError(string message = "Resource was not found.") : base(message) { }
        #endregion
    }
}