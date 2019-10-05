using Updog.Domain;

namespace Updog.Application {
    public abstract class AuthenticatedCommand : ICommand {
        #region Properties
        public User User { get; set; } = null!;
        #endregion
    }
}