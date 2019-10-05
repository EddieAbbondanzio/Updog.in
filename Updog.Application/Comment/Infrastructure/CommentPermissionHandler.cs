using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Permissions checker to see if a user has permission to edit a comment.
    /// </summary>
    public sealed class CommentPermissionHandler : PermissionHandler<Comment> {
        #region Constructor(s)
        public CommentPermissionHandler(IAdminConfig adminConfig) : base(adminConfig) {
        }
        #endregion

#pragma warning disable 1998
        protected override async Task<bool> HasPermissionTo(User user, PermissionAction action, Comment comment) {
            switch (action) {
                case PermissionAction.UpdateComment:
                case PermissionAction.DeleteComment:
                    return user.Id == comment.UserId;
                default:
                    throw new NotSupportedException();
            }
        }
#pragma warning restore 1998
    }
}