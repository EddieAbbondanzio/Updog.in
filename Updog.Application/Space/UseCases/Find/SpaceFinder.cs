using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find spaces.
    /// </summary>
    public sealed class SpaceFinder : IInteractor<SpaceFindParams, PagedResultSet<SpaceView>> {
        #region Fields
        private ISpaceRepo _repo;

        private ISpaceViewMapper _mapper;
        #endregion

        #region Constructor(s)
        public SpaceFinder(ISpaceRepo repo, ISpaceViewMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }
        #endregion

        #region Publics
        public async Task<PagedResultSet<SpaceView>> Handle(SpaceFindParams input) {
            PagedResultSet<Space> spaces = await _repo.Find(input.PageNumber, input.PageSize);

            List<SpaceView> views = new List<SpaceView>();

            foreach (Space s in spaces) {
                views.Add(_mapper.Map(s));
            }

            return new PagedResultSet<SpaceView>(views, spaces.Pagination);
        }
        #endregion
    }
}