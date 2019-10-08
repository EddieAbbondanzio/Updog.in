using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interface for a permission handler to implement to control access to various
    /// resources a user may want to interact with.
    /// </summary>
    /// <typeparam name="TResource">The type of resource being guarded.</typeparam>
    public abstract class PermissionHandler<TResource> {
        #region Fields
        private IAdminConfig adminConfig;
        #endregion

        #region Constructor(s)
        public PermissionHandler(IAdminConfig adminConfig) {
            this.adminConfig = adminConfig;
        }
        #endregion

        /// <summary>
        /// Check to see if the user has permissions on the specific resource to perform said action.
        /// </summary>
        /// <param name="user">The user to check for.</param>
        /// <param name="action">The action being performed.</param>
        /// <param name="resource">The resource being acted upon.</param>
        /// <returns>True if the user has permission to do the action.</returns>
        public async Task<bool> HasPermission(User user, PermissionAction action, TResource resource) {
            // Admin can do anything!
            if (user.Username == adminConfig.Username) {
                return true;
            } else {
                return await HasPermissionTo(user, action, resource);
            }
        }

        protected abstract Task<bool> HasPermissionTo(User user, PermissionAction action, TResource resource);
    }
}