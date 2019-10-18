using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class AddModeratorToSpaceCommandPolicy : IPolicy<AddModeratorToSpaceCommand> {
        #region Fields
        private IRoleService roleService;
        #endregion

        #region Constructor(s)
        public AddModeratorToSpaceCommandPolicy(IRoleService roleService) {
            this.roleService = roleService;
        }
        #endregion

        #region Publics
        public async Task<PolicyResult> Authorize(AddModeratorToSpaceCommand action) {
            if (await roleService.IsUserModerator(action.User.Username, action.Space)) {
                return PolicyResult.Authorized();
            } else {
                return PolicyResult.Unauthorized();
            }
        }
        #endregion
    }
}