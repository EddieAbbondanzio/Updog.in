using System.Collections.Generic;
using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IRoleRepo : IRepo<Role> {
        #region Publics
        Task<Role?> FindAdminRole(User user);
        Task<Role?> FindModeratorRole(User user, string space);
        #endregion
    }
}