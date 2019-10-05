using Updog.Domain;

namespace Updog.Application {
    public abstract class AuthenticatedQuery : IQuery {
        #region Properties
        public User User { get; set; } = null!;
        #endregion
    }
}