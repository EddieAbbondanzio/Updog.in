using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find a space via it's name.
    /// </summary>
    public sealed class SpaceFinderByName : IInteractor<string, SpaceView?> {
        #region Fields
        private ISpaceRepo _spaceRepo;

        private ISpaceViewMapper _spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceFinderByName(ISpaceRepo spaceRepo, ISpaceViewMapper spaceMapper) {
            _spaceRepo = spaceRepo;
            _spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        public async Task<SpaceView?> Handle(string input) {
            Space? s = await _spaceRepo.FindByName(input);
            return s != null ? _spaceMapper.Map(s) : null;
        }
        #endregion
    }
}