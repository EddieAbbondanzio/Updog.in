using System.Collections.Generic;
using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IPermissionReader : IReader<PermissionReadView> {
        Task<IEnumerable<PermissionReadView>> FindByRole(int roleId);
    }
}