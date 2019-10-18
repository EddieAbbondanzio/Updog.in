using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostAlterCommandPolicy : IPolicy<PostAlterCommand> {
        #region Fields
        private IPostService postService;
        private ISpaceService spaceService;
        private IRoleService roleService;
        #endregion

        #region Constructor(s)
        public PostAlterCommandPolicy(IPostService postService, ISpaceService spaceService, IRoleService roleService) {
            this.postService = postService;
            this.spaceService = spaceService;
            this.roleService = roleService;
        }
        #endregion

        public async Task<PolicyResult> Authorize(PostAlterCommand action) {
            // Check if user owns Post
            if (await postService.IsOwner(action.PostId, action.User.Username)) {
                return PolicyResult.Authorized();
            }

            // Is the user an admin?
            if (await roleService.IsUserAdmin(action.User.Username)) {
                return PolicyResult.Authorized();
            }

            // Is the user a moderator?
            Space? space = await spaceService.FindByPost(action.PostId);

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