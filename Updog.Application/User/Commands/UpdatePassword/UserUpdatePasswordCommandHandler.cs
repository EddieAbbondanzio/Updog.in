using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class UserUpdatePasswordCommandHandler : CommandHandler<UserUpdatePasswordCommand> {
        #region Fields
        private IPasswordHasher passwordHasher;
        #endregion

        #region Constructor(s)
        public UserUpdatePasswordCommandHandler(IDatabase database, IPasswordHasher passwordHasher) : base(database) {
            this.passwordHasher = passwordHasher;
        }
        #endregion

        [Validate(typeof(UserUpdatePasswordCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<UserUpdatePasswordCommand> context) {
            IUserRepo userRepo = context.Database.GetRepo<IUserRepo>();

            //Verify the old password is a match first
            bool isMatch = passwordHasher.Verify(context.Input.CurrentPassword, context.Input.User.PasswordHash);

            if (isMatch) {
                context.Input.User.PasswordHash = passwordHasher.Hash(context.Input.NewPassword);
                await userRepo.Update(context.Input.User);
                context.Output.Success("Password was updated.");
            } else {
                context.Output.BadInput("Invalid username and or password");
            }
        }
    }
}