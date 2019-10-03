using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceCreateCommandHandler : CommandHandler<SpaceCreateCommand> {
        #region Fields
        private PermissionHandler<Space> spacePermissionHandler;
        private ISpaceViewMapper spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceCreateCommandHandler(IDatabase database, PermissionHandler<Space> spacePermissionHandler, ISpaceViewMapper spaceMapper) : base(database) {
            this.spacePermissionHandler = spacePermissionHandler;
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(SpaceCreateCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<SpaceCreateCommand> context) {
            ISpaceRepo spaceRepo = context.Database.GetRepo<ISpaceRepo>();
            Space? existing = await spaceRepo.FindByName(context.Input.Name);

            if (existing != null) {
                context.Output.BadInput($"Space name {context.Input.Name} is already taken");
                return;
            }

            Space s = new Space() {
                Name = context.Input.Name,
                Description = context.Input.Description,
                User = context.Input.User,
                CreationDate = DateTime.UtcNow
            };

            await spaceRepo.Add(s);
            context.Output.Success(spaceMapper.Map(s));
        }
        #endregion
    }
}