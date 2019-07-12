using System.Threading.Tasks;
using Blurtle.Domain;

namespace Blurtle.Application {
    public sealed class FindUserByUsernameInteractor : IRequestHandler<string, User> {
        #region Fields
        private IUserRepo userRepo;
        #endregion

        #region Constructor(s)
        public FindUserByUsernameInteractor(IUserRepo userRepo) { this.userRepo = userRepo; }
        #endregion

        public async Task<User> Handle(string username) => await userRepo.FindByUsername(username);
    }
}