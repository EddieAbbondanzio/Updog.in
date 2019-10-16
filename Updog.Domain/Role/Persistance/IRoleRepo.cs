using System.Collections.Generic;
using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IRoleRepo : IRepo<Role> {
        #region Publics
        Task<IEnumerable<Role>> FindByUser(int userId);
        Task<Role> FindByUserAndDomain(int userId, string domain);
        #endregion
    }
}