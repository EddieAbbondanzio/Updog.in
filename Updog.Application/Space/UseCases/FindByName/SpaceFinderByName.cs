using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find a space via it's name.
    /// </summary>
    public sealed class SpaceFinderByName : Interactor<FindByValueParams<string>, SpaceView?> {
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
        [Validate(typeof(FindBySpaceValidator))]
        protected override async Task<SpaceView?> HandleInput(FindByValueParams<string> input) {
            using (var connection = database.GetConnection()) {
                ISpaceRepo spaceRepo = database.GetRepo<ISpaceRepo>(connection);

                Space? s = await spaceRepo.FindByName(input.Value);
                return s != null ? spaceMapper.Map(s) : null;
            }
        }
        #endregion
    }
}