using Updog.Domain;

namespace Updog.Application {
    public sealed class AdminRegisterOrUpdateCommand : AnonymousCommand {
        #region Properties
        public IAdminConfig Config { get; }
        #endregion

        #region Constructor(s)
        public AdminRegisterOrUpdateCommand(IAdminConfig config) {
            Config = config;
        }
        #endregion
    }
}