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
        protected async override Task<CommandResult> ExecuteCommand(UserUpdatePasswordCommand command) {
            User u = await service.UpdatePassword(command.Data, command.User);
            return new CommandResult(true);
        }
    }
}