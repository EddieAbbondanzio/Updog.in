using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Permissions checker to see if a user has permission to edit a comment.
    /// </summary>
    public sealed class CommentPermissionHandler : IPermissionHandler<Comment> {
#pragma warning disable 1998
        public async Task<bool> HasPermission(User user, PermissionAction action, Comment comment) {
            switch (action) {
                case PermissionAction.UpdatePost:
                    return user.Id == comment.UserId;
                case PermissionAction.DeletePost:
                    return user.Id == comment.UserId;
                default:
                    throw new NotSupportedException();
            }
        }
#pragma warning restore 1998
    }
}