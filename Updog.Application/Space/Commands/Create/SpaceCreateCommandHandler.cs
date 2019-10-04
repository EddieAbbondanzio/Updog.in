using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceCreateCommandHandler : CommandHandler<SpaceCreateCommand> {
        #region Fields
        private ISpaceFactory spaceFactory;
        private ISpaceViewMapper spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceCreateCommandHandler(IDatabase database, ISpaceFactory spaceFactory, ISpaceViewMapper spaceMapper) : base(database) {
            this.spaceFactory = spaceFactory;
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(SpaceCreateCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<SpaceCreateCommand> context) {
            ISpaceRepo spaceRepo = context.Database.GetRepo<ISpaceRepo>();
            Space? existing = await spaceRepo.FindByName(context.Input.CreationData.Name);

            if (existing != null) {
                context.Output.BadInput($"Space name {context.Input.CreationData.Name} is already taken");
                return;
            }

            Space s = spaceFactory.Create(context.Input.CreationData, context.Input.User);

            await spaceRepo.Add(s);
            context.Output.Success(spaceMapper.Map(s));
        }
        #endregion
    }
}