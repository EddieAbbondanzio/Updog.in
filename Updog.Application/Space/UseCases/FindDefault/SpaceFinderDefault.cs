using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find all of the default spaces.
    /// </summary>
    public sealed class SpaceFinderDefault : Interactor<FindParams, IEnumerable<SpaceView>> {
        #region Fields
        private IDatabase database;
        private ISpaceViewMapper spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceFinderDefault(IDatabase database, ISpaceViewMapper spaceMapper) {
            this.database = database;
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Helpers
        protected override async Task<IEnumerable<SpaceView>> HandleInput(FindParams input) {
            using (var connection = database.GetConnection()) {
                ISpaceRepo spaceRepo = database.GetRepo<ISpaceRepo>(connection);

                IEnumerable<Space> spaces = await spaceRepo.FindDefault();
                return spaces.Select(s => spaceMapper.Map(s));
            }
        }
        #endregion
    }
}