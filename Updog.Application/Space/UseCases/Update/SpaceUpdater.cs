
using System.Threading.Tasks;
using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to update a space.
    /// </summary>
    public sealed class SpaceUpdater : IInteractor<SpaceUpdateParams, SpaceView> {
        #region Fields
        private ISpaceRepo _spaceRepo;

        private IPermissionHandler<Space> _spacePermissionHandler;

        private AbstractValidator<SpaceUpdateParams> _spaceValidator;

        private ISpaceViewMapper _spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceUpdater(ISpaceRepo spaceRepo, IPermissionHandler<Space> spacePermissionHandler, AbstractValidator<SpaceUpdateParams> spaceValidator, ISpaceViewMapper spaceMapper) {
            _spaceRepo = spaceRepo;
            _spacePermissionHandler = spacePermissionHandler;
            _spaceValidator = spaceValidator;
            _spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        public async Task<SpaceView> Handle(SpaceUpdateParams input) {
            Space s = await this._spaceRepo.FindByName(input.Name);

            if (s == null) {
                throw new NotFoundException();
            }

            if (!(await this._spacePermissionHandler.HasPermission(input.User, PermissionAction.UpdateSpace, s))) {
                throw new AuthorizationException();
            }

            await _spaceValidator.ValidateAndThrowAsync(input);

            s.Description = input.Description;
            await _spaceRepo.Update(s);


            return _spaceMapper.Map(s);
        }
        #endregion
    }
}