using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class RemoveModeratorFromSpaceCommandPolicy : IPolicy<RemoveModeratorFromSpaceCommand> {
        #region Fields
        private IRoleService roleService;
        #endregion

        #region Constructor(s)
        public RemoveModeratorFromSpaceCommandPolicy(IRoleService roleService) {
            this.roleService = roleService;
        }
        #endregion

        #region Publics
        public async Task<PolicyResult> Authorize(RemoveModeratorFromSpaceCommand action) {
            if (await roleService.IsUserModerator(action.User.Username, action.Space)) {
                return PolicyResult.Authorized();
            } else {
                return PolicyResult.Unauthorized();
            }
        }
        #endregion
    }
}