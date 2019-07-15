using System;
using System.Threading.Tasks;
using Blurtle.Domain;

namespace Blurtle.Application {
    /// <summary>
    /// Interactor to handler a login user use case.
    /// </summary>
    public sealed class LoginUserInteractor : IRequestHandler<LoginUserParams, UserLogin> {
        #region Fields
        private IUserRepo userRepo;

        private IPasswordHasher passwordHasher;

        private IAuthenticationTokenHandler tokenHandler;
        #endregion

        #region COnstructor(s)
        public LoginUserInteractor(IUserRepo userRepo, IPasswordHasher passwordHasher, IAuthenticationTokenHandler tokenHandler) {
            this.userRepo = userRepo;
            this.passwordHasher = passwordHasher;
            this.tokenHandler = tokenHandler;
        }
        #endregion

        #region Publics
        public async Task<UserLogin> Handle(LoginUserParams input) {
            User user = await userRepo.FindByUsername(input.Username);

            if (user == null) {
                return null;
            }

            if (passwordHasher.Verify(input.Password, user.PasswordHash)) {
                return new UserLogin(new UserInfo(user.Username, user.JoinedDate), tokenHandler.IssueToken(user));
            } else {
                return null;
            }
        }
        #endregion
    }
}