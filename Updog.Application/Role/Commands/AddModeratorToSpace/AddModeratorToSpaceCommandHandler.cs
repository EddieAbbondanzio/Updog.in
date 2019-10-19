using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class AddModeratorToSpaceCommandHandler : CommandHandler<AddModeratorToSpaceCommand> {
        #region Fields
        private IRoleService roleService;
        #endregion

        #region Constructor(s)
        public AddModeratorToSpaceCommandHandler(IRoleService roleService) {
            this.roleService = roleService;
        }
        #endregion

        #region Privates
        [Validate(typeof(AddModeratorToSpaceCommandValidator))]
        [Policy(typeof(AddModeratorToSpaceCommandPolicy))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(AddModeratorToSpaceCommand command) {
            await roleService.AddModeratorToSpace(command.Username, command.Space, command.User);
            return Success();
        }
        #endregion
    }
}