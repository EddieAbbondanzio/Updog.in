using System.Threading.Tasks;
using Blurtle.Domain;

namespace Blurtle.Application {
    public sealed class FindUserByUsernameInteractor : IRequestHandler<string, UserInfo> {
        #region Fields
        private IUserRepo userRepo;
        #endregion

        #region Constructor(s)
        public FindUserByUsernameInteractor(IUserRepo userRepo) { this.userRepo = userRepo; }
        #endregion

        public async Task<UserInfo> Handle(string username) {
            User user = await userRepo.FindByUsername(username);
            return user != null ? new UserInfo(user.Username, user.JoinedDate) : null;
        }
    }
}