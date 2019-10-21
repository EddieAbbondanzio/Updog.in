using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class UserUpdateCommandHandler : CommandHandler<UserUpdateCommand> {
        #region Fields
        private IUserService service;
        #endregion    

        #region Constructor(s)
        public UserUpdateCommandHandler(IUserService service) {
            this.service = service;
        }
        #endregion

        [Validate(typeof(UserUpdateCommandValidator))]
        [Policy(typeof(UserAlterCommandPolicy))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(UserUpdateCommand command) {
            if (!await service.DoesUserExist(command.Username)) {
                return new NotFoundError($"User {command.Username} not found.");
            }

            await service.Update(command.Username, command.Update);
            return Success();
        }
    }
}