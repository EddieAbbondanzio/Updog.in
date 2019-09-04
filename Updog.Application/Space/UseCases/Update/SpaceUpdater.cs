
using System.Threading.Tasks;
using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to update a space.
    /// </summary>
    public sealed class SpaceUpdater : IInteractor<SpaceUpdateParams, SpaceView> {
        #region Fields
        private ISpaceRepo spaceRepo;

        private IPermissionHandler<Space> spacePermissionHandler;

        private AbstractValidator<SpaceUpdateParams> spaceValidator;

        private IMapper<Space, SpaceView> spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceUpdater(ISpaceRepo spaceRepo, IPermissionHandler<Space> spacePermissionHandler, AbstractValidator<SpaceUpdateParams> spaceValidator, IMapper<Space, SpaceView> spaceMapper) {
            this.spaceRepo = spaceRepo;
            this.spacePermissionHandler = spacePermissionHandler;
            this.spaceValidator = spaceValidator;
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        public async Task<SpaceView> Handle(SpaceUpdateParams input) {
            Space s = await this.spaceRepo.FindByName(input.Name);

            if (s == null) {
                throw new NotFoundException();
            }

            if (!(await this.spacePermissionHandler.HasPermission(input.User, PermissionAction.UpdateSpace, s))) {
                throw new AuthorizationException();
            }

            await spaceValidator.ValidateAndThrowAsync(input);

            s.Description = input.Description;
            await spaceRepo.Update(s);


            return spaceMapper.Map(s);
        }
        #endregion
    }
}