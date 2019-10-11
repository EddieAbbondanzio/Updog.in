using System.Collections.Generic;
using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IRoleReader : IReader<RoleReadView> {
        /// <summary>
        /// Find the roles of a specific user.
        /// </summary>
        /// <param name="userId">The user to get roles for.</param>
        /// <returns>The collection of their roles.</returns>
        Task<IEnumerable<RoleReadView>> FindByUser(int userId);
    }
}