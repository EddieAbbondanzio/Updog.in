using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class LoginUserCommandHandler : CommandHandler<LoginUserCommand> {
        #region Fields
        private IPasswordHasher passwordHasher;
        private IAuthenticationTokenHandler tokenHandler;
        private IUserViewMapper userMapper;

        public LoginUserCommandHandler(IDatabase database, IPasswordHasher passwordHasher, IAuthenticationTokenHandler tokenHandler, IUserViewMapper userMapper) : base(database) {
            this.passwordHasher = passwordHasher;
            this.tokenHandler = tokenHandler;
            this.userMapper = userMapper;
        }
        #endregion

        [Validate(typeof(LoginUserCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<LoginUserCommand> context) {
            IUserRepo userRepo = context.Database.GetRepo<IUserRepo>();

            User? user = await userRepo.FindByUsername(context.Input.Credentials.Username);

            if (user == null) {
                context.Output.Unauthorized("Invalid username, and or password");
                return;
            }

            if (passwordHasher.Verify(context.Input.Credentials.Password, user.PasswordHash)) {
                UserView userView = userMapper.Map(user);
                string authToken = tokenHandler.IssueToken(user);

                context.Output.Success(new UserLogin(userView, authToken));
            } else {
                context.Output.Unauthorized("Invalid username, and or password");
            }
        }
    }
}