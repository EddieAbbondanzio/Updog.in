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
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(SpaceCreateCommand command) {
            if (!(await service.DoesSpaceExist(command.Data.Name))) {
                return new NotFoundError("");
            }

            Space s = await service.Create(command.Data, command.User);
            return Insert(s.Id);
        }
        #endregion
    }
}