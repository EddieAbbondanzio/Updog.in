using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class AddAdminCommandHandler : CommandHandler<AddAdminCommand> {
        #region Fields
        private IRoleService roleService;
        #endregion

        #region Constructor(s)
        public AddAdminCommandHandler(IRoleService roleService) {
            this.roleService = roleService;
        }
        #endregion

        #region Task<Either<CommandResult, Error>>
        [Validate(typeof(AddAdminCommandValidator))]
        [Policy(typeof(AddAdminCommandPolicy))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(AddAdminCommand command) {
            await roleService.AddAdmin(command.Username, command.User);
            return Success();
        }
        #endregion
    }
}