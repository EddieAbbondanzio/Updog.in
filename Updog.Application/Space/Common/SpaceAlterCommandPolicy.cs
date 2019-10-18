using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceAlterCommandPolicy : IPolicy<SpaceAlterCommand> {
        #region Fields
        private IRoleService roleService;
        #endregion

        #region Constructor(s)
        public SpaceAlterCommandPolicy(IRoleService roleService) {
            this.roleService = roleService;
        }
        #endregion

        #region Publics
        public async Task<PolicyResult> Authorize(SpaceAlterCommand action) {
            if (await roleService.IsUserAdmin(action.User.Username)) {
                return PolicyResult.Authorized();
            }

            if (await roleService.IsUserModerator(action.User.Username, action.Space)) {
                return PolicyResult.Authorized();
            }

            return PolicyResult.Unauthorized();
        }
        #endregion
    }
}