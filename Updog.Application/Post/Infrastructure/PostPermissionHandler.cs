using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Permissions checker to see if a user has permission to edit a post.
    /// </summary>
    public sealed class PostPermissionHandler : PermissionHandler<Post> {
        #region Constructor(s)
        public PostPermissionHandler(IAdminConfig adminConfig) : base(adminConfig) { }
        #endregion

#pragma warning disable 1998
        protected override async Task<bool> HasPermissionTo(User user, PermissionAction action, Post post) {
            switch (action) {
                case PermissionAction.UpdatePost:
                case PermissionAction.DeletePost:
                    return user.Equals(post.User);
                default:
                    throw new NotSupportedException();
            }
        }
#pragma warning restore 1998
    }
}