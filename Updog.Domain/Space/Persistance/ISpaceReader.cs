using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Domain.Paging;

namespace Updog.Domain {
    public interface ISpaceReader : IReader<SpaceReadView> {
        #region Publics
        Task<PagedResultSet<SpaceReadView>> Find(PaginationInfo paging);
        Task<SpaceReadView?> FindById(int id);
        Task<SpaceReadView?> FindByName(string name);

        Task<IEnumerable<SpaceReadView>> FindDefault();

        Task<IEnumerable<SpaceReadView>> FindSubscribed(User user);
        Task<IEnumerable<SpaceReadView>> FindSpacesUserModerates(string username);
        #endregion
    }
}