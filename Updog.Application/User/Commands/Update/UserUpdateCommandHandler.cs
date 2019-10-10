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
        protected async override Task<CommandResult> ExecuteCommand(UserUpdateCommand command) {
            try {
                User user = await service.Update(command.Data, command.User);
                return Success();
            } catch (InvalidOperationException) {
                return Failure("No user with matching Id found.");
            }
        }
    }
}