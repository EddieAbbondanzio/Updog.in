
using System;
using System.Threading.Tasks;
using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to update a space.
    /// </summary>
    public sealed class SpaceUpdater : IInteractor<SpaceUpdateParams, SpaceView> {
        #region Fields
        private IDatabase database;
        private IPermissionHandler<Space> spacePermissionHandler;
        private IValidator<SpaceUpdateParams> spaceValidator;
        private ISpaceViewMapper spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceUpdater(IDatabase database, IPermissionHandler<Space> spacePermissionHandler, IValidator<SpaceUpdateParams> spaceValidator, ISpaceViewMapper spaceMapper) {
            this.database = database;
            this.spacePermissionHandler = spacePermissionHandler;
            this.spaceValidator = spaceValidator;
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        public async Task<SpaceView> Handle(SpaceUpdateParams input) {
            await spaceValidator.ValidateAndThrowAsync(input);

            using (var connection = database.GetConnection()) {
                ISpaceRepo spaceRepo = database.GetRepo<ISpaceRepo>(connection);

                Space? s = await spaceRepo.FindByName(input.Name);

                if (s == null) {
                    throw new InvalidOperationException();
                }

                if (!(await this.spacePermissionHandler.HasPermission(input.User, PermissionAction.UpdateSpace, s))) {
                    throw new AuthorizationException();
                }

                s.Description = input.Description;
                await spaceRepo.Update(s);

                return spaceMapper.Map(s);
            }
        }
        #endregion
    }
}