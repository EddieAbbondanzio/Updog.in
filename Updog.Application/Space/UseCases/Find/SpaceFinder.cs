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
        private ISpaceRepo repo;

        private IMapper<Space, SpaceView> mapper;
        #endregion

        #region Constructor(s)
        public SpaceFinder(ISpaceRepo repo, IMapper<Space, SpaceView> mapper) {
            this.repo = repo;
            this.mapper = mapper;
        }
        #endregion

        #region Publics
        public async Task<PagedResultSet<SpaceView>> Handle(SpaceFindParams input) {
            PagedResultSet<Space> spaces = await repo.Find(input.PageNumber, input.PageSize);

            List<SpaceView> views = new List<SpaceView>();

            foreach (Space s in spaces) {
                views.Add(mapper.Map(s));
            }

            return new PagedResultSet<SpaceView>(views, spaces.Pagination);
        }
        #endregion
    }
}