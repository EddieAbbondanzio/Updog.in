using Updog.Domain;

namespace Updog.Api {
    public sealed class AdminConfig : IAdminConfig {
        #region Properties
        public string Username { get; set; } = "";

        public string Password { get; set; } = "";
        #endregion
    }
}