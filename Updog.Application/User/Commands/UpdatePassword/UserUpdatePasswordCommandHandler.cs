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
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(UserUpdatePasswordCommand command) {
            if (!await service.DoesUserExist(command.Username)) {
                return new NotFoundError($"User {command.Username} not found.");
            }

            await service.UpdatePassword(command.Username, command.UpdatePassword);
            return Success();
        }
    }
}