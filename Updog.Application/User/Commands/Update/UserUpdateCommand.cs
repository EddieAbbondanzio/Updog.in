using Updog.Domain;

namespace Updog.Application {
    public sealed class UserUpdateCommand : AuthenticatedCommand {
        #region Properties
        public string Email { get; set; } = "";
        #endregion
    }
}