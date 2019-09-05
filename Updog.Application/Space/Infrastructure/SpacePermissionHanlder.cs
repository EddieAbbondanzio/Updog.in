using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Permissions checker to see if a user has permission to edit a post.
    /// </summary>
    public sealed class SpacePermissionHandler : IPermissionHandler<Space> {
#pragma warning disable 1998
        public async Task<bool> HasPermission(User user, PermissionAction action, Space space) {
            switch (action) {
                case PermissionAction.UpdateSpace:
                case PermissionAction.DeleteSpace:
                    return user.Equals(space.User);
                default:
                    throw new NotSupportedException();
            }
        }
#pragma warning restore 1998
    }
}