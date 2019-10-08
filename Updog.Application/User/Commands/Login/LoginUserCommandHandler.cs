using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class LoginUserCommandHandler : CommandHandler<LoginUserCommand> {
        #region Fields
        private IUserService service;
        #endregion

        #region Constructor(s)
        public LoginUserCommandHandler(IUserService service) {
            this.service = service;
        }
        #endregion

        [Validate(typeof(LoginUserCommandValidator))]
        protected async override Task<CommandResult> ExecuteCommand(LoginUserCommand command) {
            UserLogin? login = await service.Login(command.Credentials);

            if (login != null) {
                return new DataResult<UserLogin>(true, login);
            } else {
                return new CommandResult(false);
            }
        }
    }
}