using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceUpdateCommandHandler : CommandHandler<SpaceUpdateCommand> {
        #region Fields
        private PermissionHandler<Space> spacePermissionHandler;
        private ISpaceViewMapper spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceUpdateCommandHandler(IDatabase database, PermissionHandler<Space> spacePermissionHandler, ISpaceViewMapper spaceMapper) : base(database) {
            this.spacePermissionHandler = spacePermissionHandler;
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(SpaceUpdateCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<SpaceUpdateCommand> context) {
            ISpaceRepo spaceRepo = context.Database.GetRepo<ISpaceRepo>();

            Space? s = await spaceRepo.FindByName(context.Input.Name);

            if (s == null) {
                context.Output.BadInput($"No space with name: {context.Input.Name} exists");
                return;
            }

            if (!(await this.spacePermissionHandler.HasPermission(context.Input.User, PermissionAction.UpdateSpace, s))) {
                context.Output.Unauthorized("Unauthorized");
                return;
            }

            s.Description = context.Input.Description;
            await spaceRepo.Update(s);
            context.Output.Success(spaceMapper.Map(s));
        }
        #endregion
    }
}