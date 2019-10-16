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

        #region Privates
        [Validate(typeof(AddAdminCommandValidator))]
        protected async override Task<CommandResult> ExecuteCommand(AddAdminCommand command) {
            await roleService.AddAdmin(command.Username, command.User);
            return Success();
        }
        #endregion
    }
}