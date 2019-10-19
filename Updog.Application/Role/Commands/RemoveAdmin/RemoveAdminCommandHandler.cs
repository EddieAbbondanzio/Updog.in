using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class RemoveAdminCommandHandler : CommandHandler<RemoveAdminCommand> {
        #region Fields
        private IRoleService roleService;
        #endregion

        #region Constructor(s)
        public RemoveAdminCommandHandler(IRoleService roleService) {
            this.roleService = roleService;
        }
        #endregion

        #region Privates
        [Validate(typeof(RemoveAdminCommandValidator))]
        [Policy(typeof(RemoveAdminCommandPolicy))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(RemoveAdminCommand command) {
            await roleService.RemoveAdmin(command.Username, command.User);
            return Success();
        }
        #endregion
    }
}