using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class RemoveAdminCommandPolicy : IPolicy<RemoveAdminCommand> {
        #region Fields
        private IRoleService roleService;
        #endregion

        #region Constructor(s)
        public RemoveAdminCommandPolicy(IRoleService roleService) {
            this.roleService = roleService;
        }
        #endregion

        #region Publics
        public async Task<PolicyResult> Authorize(RemoveAdminCommand action) {
            if (await roleService.IsUserAdmin(action.User.Username)) {
                return PolicyResult.Authorized();
            } else {
                return PolicyResult.Unauthorized();
            }
        }
        #endregion
    }
}