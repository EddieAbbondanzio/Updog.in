using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find spaces.
    /// </summary>
    public sealed class SpaceFinder : IInteractor<SpaceFindParams, PagedResultSet<SpaceView>> {
        #region Fields
        private IDatabase database;
        private ISpaceViewMapper mapper;
        #endregion

        #region Constructor(s)
        public SpaceFinder(IDatabase database, ISpaceViewMapper mapper) {
            this.database = database;
            this.mapper = mapper;
        }
        #endregion

        #region Publics
        public async Task<PagedResultSet<SpaceView>> Handle(SpaceFindParams input) {
            using (var connection = database.GetConnection()) {
                ISpaceRepo spaceRepo = database.GetRepo<ISpaceRepo>(connection);

                PagedResultSet<Space> spaces = await spaceRepo.Find(input.PageNumber, input.PageSize);
                return new PagedResultSet<SpaceView>(spaces.Select(s => mapper.Map(s)), spaces.Pagination);
            }
        }
        #endregion
    }
}