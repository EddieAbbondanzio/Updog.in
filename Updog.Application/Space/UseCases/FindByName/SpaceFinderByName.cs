using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find a space via it's name.
    /// </summary>
    public sealed class SpaceFinderByName : IInteractor<string, SpaceView?> {
        #region Fields
        private IDatabase database;
        private ISpaceViewMapper spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceFinderByName(IDatabase database, ISpaceViewMapper spaceMapper) {
            this.database = database;
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        public async Task<SpaceView?> Handle(string input) {
            using (var connection = database.GetConnection()) {
                ISpaceRepo spaceRepo = database.GetRepo<ISpaceRepo>(connection);

                Space? s = await spaceRepo.FindByName(input);
                return s != null ? spaceMapper.Map(s) : null;
            }
        }
        #endregion
    }
}