using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentAlterCommandPolicy : IPolicy<CommentAlterCommand> {
        #region Fields
        private ICommentService commentService;
        private ISpaceService spaceService;
        private IRoleService roleService;
        #endregion

        #region Constructor(s)
        public CommentAlterCommandPolicy(ICommentService commentService, ISpaceService spaceService, IRoleService roleService) {
            this.commentService = commentService;
            this.spaceService = spaceService;
            this.roleService = roleService;
        }
        #endregion

        public async Task<PolicyResult> Authorize(CommentAlterCommand action) {
            // Check if user owns comment
            if (await commentService.IsOwner(action.CommentId, action.User.Username)) {
                return PolicyResult.Authorized();
            }

            // Is the user an admin?
            if (await roleService.IsUserAdmin(action.User.Username)) {
                return PolicyResult.Authorized();
            }

            // Is the user a moderator?
            Space? space = await spaceService.FindByComment(action.CommentId);

            if (space == null) {
                throw new InvalidOperationException();
            }

            if (await roleService.IsUserModerator(action.User.Username, space.Name)) {
                return PolicyResult.Authorized();
            }

            return PolicyResult.Unauthorized();
        }
    }
}