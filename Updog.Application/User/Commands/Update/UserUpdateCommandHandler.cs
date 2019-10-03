using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class UserUpdateCommandHandler : CommandHandler<UserUpdateCommand> {
        #region Constructor(s)
        public UserUpdateCommandHandler(IDatabase database) : base(database) {
        }
        #endregion

        [Validate(typeof(UserUpdateCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<UserUpdateCommand> context) {
            IUserRepo userRepo = context.Database.GetRepo<IUserRepo>();

            //Is the email already in use?
            User? existing = await userRepo.FindByEmail(context.Input.Email);

            if (!existing?.Equals(context.Input.User) ?? false) {
                context.Output.BadInput("Email is already in use");
                return;
            }

            // Good to go, update and save off the change.
            context.Input.User.Email = context.Input.Email;
            await userRepo.Update(context.Input.User);
            context.Output.Success("User was updated.");
        }
    }
}