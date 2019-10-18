using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class UserUpdatePasswordCommandHandler : CommandHandler<UserUpdatePasswordCommand> {
        #region Fields
        private IUserService service;
        #endregion

        #region Constructor(s)
        public UserUpdatePasswordCommandHandler(IUserService service) {
            this.service = service;
        }
        #endregion

        [Validate(typeof(UserUpdatePasswordCommandValidator))]
        [Policy(typeof(UserAlterCommandPolicy))]
        protected async override Task<CommandResult> ExecuteCommand(UserUpdatePasswordCommand command) {
            try {
                User u = await service.UpdatePassword(command.Username, command.UpdatePassword);
                return Success();
            } catch (UnauthorizedAccessException) {
                return Failure("Password does not match the one on file.");
            }
        }
    }
}