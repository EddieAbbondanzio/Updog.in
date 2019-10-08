using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceUpdateCommandHandler : CommandHandler<SpaceUpdateCommand> {
        #region Fields
        private ISpaceService service;
        #endregion

        #region Constructor(s)
        public SpaceUpdateCommandHandler(ISpaceService service) {
            this.service = service;
        }
        #endregion

        #region Publics
        [Validate(typeof(SpaceUpdateCommandValidator))]
        protected async override Task<CommandResult> ExecuteCommand(SpaceUpdateCommand command) {
            Space s = await service.Update(new SpaceUpdateData(command.Name, command.Description), command.User);
            return new CommandResult(true);
        }
        #endregion
    }
}