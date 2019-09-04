using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find a space via it's name.
    /// </summary>
    public sealed class SpaceFinderByName : IInteractor<string, SpaceView> {
        #region Fields
        private ISpaceRepo spaceRepo;

        private IMapper<Space, SpaceView> spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceFinderByName(ISpaceRepo spaceRepo, IMapper<Space, SpaceView> spaceMapper) {
            this.spaceRepo = spaceRepo;
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        public async Task<SpaceView> Handle(string input) {
            Space s = await spaceRepo.FindByName(input);
            return s != null ? spaceMapper.Map(s) : null;
        }
        #endregion
    }
}