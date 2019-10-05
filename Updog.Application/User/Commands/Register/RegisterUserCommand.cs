
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Request object to create a new user with the system.
    /// </summary>
    public sealed class RegisterUserCommand : AnonymousCommand {
        #region Properties
        public UserRegistration Registration { get; set; } = null!;
        #endregion
    }
}