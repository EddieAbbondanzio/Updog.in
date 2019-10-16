using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class RemoveModeratorFromSpaceCommandHandler : CommandHandler<RemoveModeratorFromSpaceCommand> {
        #region Fields
        private IRoleService roleService;
        #endregion

        #region Constructor(s)
        public RemoveModeratorFromSpaceCommandHandler(IRoleService roleService) {
            this.roleService = roleService;
        }
        #endregion

        #region Privates
        [Validate(typeof(AddModeratorToSpaceCommandValidator))]
        protected async override Task<CommandResult> ExecuteCommand(RemoveModeratorFromSpaceCommand command) {
            await roleService.RemoveModeratorFromSpace(command.Username, command.Space, command.User);
            return Success();
        }
        #endregion
    }
}