using System.Threading.Tasks;
using Blurtle.Domain;

namespace Blurtle.Application {
    /// <summary>
    /// Interface for a permission handler to implement to control access to various
    /// resources a user may want to interact with.
    /// </summary>
    /// <typeparam name="TResource">The type of resource being guarded.</typeparam>
    public interface IPermissionHandler<TResource> {
        /// <summary>
        /// Check to see if the user has permissions on the specific resource to perform said action.
        /// </summary>
        /// <param name="user">The user to check for.</param>
        /// <param name="action">The action being performed.</param>
        /// <param name="resource">The resource being acted upon.</param>
        /// <returns>True if the user has permission to do the action.</returns>
        Task<bool> HasPermission(User user, PermissionAction action, TResource resource);
    }
}