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
        protected async override Task<CommandResult> ExecuteCommand(RegisterUserCommand command) {
            UserLogin? login = await service.Register(command.Registration);

            if (login != null) {
                return new DataResult<UserLogin>(true, login);
            } else {
                return new CommandResult(false);
            }
        }
    }
}