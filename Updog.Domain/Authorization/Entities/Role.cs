using System.Collections.Generic;
using System.Linq;

namespace Updog.Domain {
    public sealed class Role : IEntity {
        #region Constants
        /// <summary>
        /// Literal used to represent a site-wide role vs a space role.
        /// </summary>
        public const string SiteWideScope = "*";
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Scope { get; }
        public IEnumerable<Permission> Permissions { get; }
        #endregion

        #region Constructor(s)
        public Role(string scope, IEnumerable<Permission> permissions) {
            Scope = scope;
            Permissions = permissions;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Check to see if the role has permissions to perform a specific action
        /// on a resource.
        /// </summary>
        /// <param name="resource">The resource type.</param>
        /// <param name="action">The desired action.</param>
        /// <returns>True if the role can do the action.</returns>
        public bool HasPermission(PermissionResource resource, PermissionAction action) => Permissions.Any(p => p.Resource == resource && p.Action == action);
        #endregion

        #region Statics
        /// <summary>
        /// Create a new site wide role.
        /// </summary>
        /// <param name="permissions">The permissions of the role.</param>
        /// <returns>The newly created role.</returns>
        public static Role SiteWide(IEnumerable<Permission> permissions) => new Role(SiteWideScope, permissions);

        /// <summary>
        /// Create a new space role.
        /// </summary>
        /// <param name="space">The space it applies to.</param>
        /// <param name="permissions">The permissions of the role.</param>
        /// <returns>The newly created role.</returns>
        public static Role Space(string space, IEnumerable<Permission> permissions) => new Role(space, permissions);
        #endregion
    }
}