using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceCreateCommandHandler : CommandHandler<SpaceCreateCommand> {
        #region Fields
        private ISpaceService service;
        #endregion

        #region Constructor(s)
        public SpaceCreateCommandHandler(ISpaceService service) {
            this.service = service;
        }
        #endregion

        #region Publics
        [Validate(typeof(SpaceCreateCommandValidator))]
        protected async override Task<CommandResult> ExecuteCommand(SpaceCreateCommand command) {
            Space s = await service.Create(command.Data, command.User);
            return new InsertResult(true, s.Id);
        }
        #endregion
    }
}