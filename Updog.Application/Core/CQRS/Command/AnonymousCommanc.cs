using Updog.Domain;

namespace Updog.Application {
    public abstract class AnonymousCommand : ICommand {
        #region Properties
        public User? User { get; set; }
        #endregion
    }
}