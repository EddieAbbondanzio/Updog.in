
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Request object to create a new user with the system.
    /// </summary>
    public sealed class RegisterUserCommand : ICommand {
        #region Properties
        public UserRegistration Registration { get; }
        #endregion

        #region Constructor(s)
        public RegisterUserCommand(UserRegistration registration) {
            Registration = registration;
        }
        #endregion
    }
}