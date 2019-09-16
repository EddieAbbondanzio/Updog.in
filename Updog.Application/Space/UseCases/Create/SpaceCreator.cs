using System;
using System.Threading.Tasks;
using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to create a new space.
    /// </summary>
    public sealed class SpaceCreator : IInteractor<SpaceCreateParams, SpaceView> {
        #region Fields
        private IDatabase database;
        private IValidator<SpaceCreateParams> spaceValidator;
        private ISpaceViewMapper spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceCreator(IDatabase database, IValidator<SpaceCreateParams> spaceValidator, ISpaceViewMapper spaceMapper) {
            this.database = database;
            this.spaceValidator = spaceValidator;
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        public async Task<SpaceView> Handle(SpaceCreateParams input) {
            await spaceValidator.ValidateAndThrowAsync(input);

            using (var connection = database.GetConnection()) {
                ISpaceRepo spaceRepo = database.GetRepo<ISpaceRepo>(connection);
                Space? existing = await spaceRepo.FindByName(input.Name);

                if (existing != null) {
                    throw new InvalidOperationException($"Space name {input.Name} is already taken.");
                }

                Space s = new Space() {
                    Name = input.Name,
                    Description = input.Description,
                    User = input.User,
                    CreationDate = DateTime.UtcNow
                };

                await spaceRepo.Add(s);
                return spaceMapper.Map(s);
            }
        }
        #endregion
    }
}