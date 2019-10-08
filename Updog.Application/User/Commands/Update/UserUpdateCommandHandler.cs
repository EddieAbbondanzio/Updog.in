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
            User user = await service.Update(new UserUpdateData(command.User, command.Email));
            return new CommandResult(true);
        }
    }
}