namespace Updog.Application {
    public sealed class AdminRegisterOrUpdateCommand : ICommand {
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