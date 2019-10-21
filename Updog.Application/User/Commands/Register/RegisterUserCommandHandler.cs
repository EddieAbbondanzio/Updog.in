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
            if (!(await service.IsUsernameAvailable(command.Registration.Username))) {
                return new Error("");
            }

            if (command.Registration.Email != null && !(await service.IsEmailAlreadyInUse(command.Registration.Email))) {
                return new Error("");
            }

            UserLogin login = await service.Register(command.Registration);
            return Success();
        }
    }
}