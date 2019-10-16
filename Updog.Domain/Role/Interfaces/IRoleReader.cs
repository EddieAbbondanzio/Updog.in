using System.Collections.Generic;
using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IRoleReader : IReader<RoleReadView> {
        #region Publics
        Task<IEnumerable<UserReadView>> FindAdmins();
        Task<IEnumerable<UserReadView>> FindModerators(string space);
        Task<IEnumerable<SpaceReadView>> FindSpacesUserModerates(string username);
        #endregion
    }
}