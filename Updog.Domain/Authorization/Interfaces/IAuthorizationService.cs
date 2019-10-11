using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IAuthorizationService : IService {
        #region Publics
        Task<bool> HasPermission(User user, PermissionResource resource, PermissionAction action);
        #endregion
    }
}