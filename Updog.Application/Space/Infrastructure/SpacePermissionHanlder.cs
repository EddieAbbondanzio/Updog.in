using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Permissions checker to see if a user has permission to edit a post.
    /// </summary>
    public sealed class SpacePermissionHandler : PermissionHandler<Space> {
        #region Constructor(s)
        public SpacePermissionHandler(IAdminConfig adminConfig) : base(adminConfig) { }
        #endregion

#pragma warning disable 1998
        protected override async Task<bool> HasPermissionTo(User user, PermissionAction action, Space space) {
            switch (action) {
                case PermissionAction.CreateSpace:
                case PermissionAction.UpdateSpace:
                case PermissionAction.DeleteSpace:
                    return false;
                default:
                    throw new NotSupportedException();
            }
        }
#pragma warning restore 1998
    }
}