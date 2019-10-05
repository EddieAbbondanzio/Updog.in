using Updog.Domain;

namespace Updog.Application {
    public abstract class AnonymousQuery : IQuery {
        #region Properties
        public User? User { get; set; }
        #endregion
    }
}