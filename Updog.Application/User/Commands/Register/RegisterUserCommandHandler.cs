using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class RegisterUserCommandHandler : CommandHandler<RegisterUserCommand> {
        #region Fields
        private IUserService service;
        #endregion

        #region Constructor(s)
        public RegisterUserCommandHandler(IUserService service) {
            this.service = service;
        }
        #endregion

        [Validate(typeof(RegisterUserCommandValidator))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(RegisterUserCommand command) {
            try {
                UserLogin login = await service.Register(command.Registration);
                return Success();
            } catch (UsernameAlreadyInUseException) {
                return Failure("Username is unavailable.");
            } catch (EmailAlreadyInUseException) {
                return Failure("Email is already in use.");
            }

        }
    }
}