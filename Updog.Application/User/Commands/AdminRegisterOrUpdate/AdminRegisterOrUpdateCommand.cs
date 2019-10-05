using Updog.Domain;

namespace Updog.Application {
    public sealed class AdminRegisterOrUpdateCommand : AnonymousCommand {
        #region Properties
        public IAdminConfig Config { get; set; } = null!;
        #endregion
    }
}