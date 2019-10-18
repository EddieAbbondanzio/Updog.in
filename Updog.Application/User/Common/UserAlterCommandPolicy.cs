using System.Threading.Tasks;

namespace Updog.Application {
    public sealed class UserAlterCommandPolicy : IPolicy<UserAlterCommand> {
#pragma warning disable 1998
        #region Publics
        public async Task<PolicyResult> Authorize(UserAlterCommand action) {
            if (action.Username == action.User.Username) {
                return PolicyResult.Authorized();
            } else {
                return PolicyResult.Unauthorized();
            }
        }
        #endregion
#pragma warning restore 1998
    }
}