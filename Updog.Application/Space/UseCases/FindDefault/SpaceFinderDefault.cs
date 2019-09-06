using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find all of the default spaces.
    /// </summary>
    public sealed class SpaceFinderDefault : IInteractor<object, IEnumerable<SpaceView>> {
        #region Fields
        private ISpaceRepo _spaceRepo;

        private ISpaceViewMapper _spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceFinderDefault(ISpaceRepo spaceRepo, ISpaceViewMapper spaceMapper) {
            _spaceRepo = spaceRepo;
            _spaceMapper = spaceMapper;
        }
        #endregion

        public async Task<IEnumerable<SpaceView>> Handle(object input) {
            IEnumerable<Space> spaces = await _spaceRepo.FindDefault();

            List<SpaceView> views = new List<SpaceView>();

            foreach (Space s in spaces) {
                views.Add(_spaceMapper.Map(s));
            }

            return views;
        }
    }
}