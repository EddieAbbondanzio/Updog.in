using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class AddAdminCommandPolicy : IPolicy<AddAdminCommand> {
        #region Fields
        private IRoleService roleService;
        #endregion

        #region Constructor(s)
        public AddAdminCommandPolicy(IRoleService roleService) {
            this.roleService = roleService;
        }
        #endregion

        #region Publics
        public async Task<PolicyResult> Authorize(AddAdminCommand action) {
            if (await roleService.IsUserAdmin(action.User.Username)) {
                return PolicyResult.Authorized();
            } else {
                return PolicyResult.Unauthorized();
            }
        }
        #endregion
    }
}