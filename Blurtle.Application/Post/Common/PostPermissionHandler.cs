using System;
using System.Threading.Tasks;
using Blurtle.Domain;

namespace Blurtle.Application {
    /// <summary>
    /// Permissions checker to see if a user has permission to edit a post.
    /// </summary>
    public sealed class PostPermissionHandler : IPermissionHandler<Post> {
#pragma warning disable 1998
        public async Task<bool> HasPermission(User user, PermissionAction action, Post post) {
            switch (action) {
                case PermissionAction.UpdatePost:
                    return user.Id == post.UserId;
                default:
                    throw new NotSupportedException();
            }
        }
#pragma warning restore 1998
    }
}