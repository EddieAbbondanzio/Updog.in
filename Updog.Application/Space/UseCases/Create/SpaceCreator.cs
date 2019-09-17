using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to create a new space.
    /// </summary>
    public sealed class SpaceCreator : Interactor<SpaceCreateParams, SpaceView> {
        #region Fields
        private IDatabase database;
        private ISpaceViewMapper spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceCreator(IDatabase database, ISpaceViewMapper spaceMapper) {
            this.database = database;
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(SpaceCreateValidator))]
        protected override async Task<SpaceView> HandleInput(SpaceCreateParams input) {
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