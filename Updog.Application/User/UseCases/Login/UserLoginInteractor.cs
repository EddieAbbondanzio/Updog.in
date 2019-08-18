using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to handler a login user use case.
    /// </summary>
    public sealed class UserLoginInteractor : IInteractor<UserLoginParams, UserLogin> {
        #region Fields
        private IUserRepo userRepo;

        private IPasswordHasher passwordHasher;

        private IAuthenticationTokenHandler tokenHandler;

        private IMapper<User, UserView> userMapper;
        #endregion

        #region Constructor(s)
        public UserLoginInteractor(IUserRepo userRepo, IMapper<User, UserView> userMapper, IPasswordHasher passwordHasher, IAuthenticationTokenHandler tokenHandler) {
            this.userRepo = userRepo;
            this.userMapper = userMapper;
            this.passwordHasher = passwordHasher;
            this.tokenHandler = tokenHandler;
        }
        #endregion

        #region Publics
        public async Task<UserLogin> Handle(UserLoginParams input) {
            User user = await userRepo.FindByUsername(input.Username);

            if (user == null) {
                return null;
            }

            if (passwordHasher.Verify(input.Password, user.PasswordHash)) {
                UserView userView = userMapper.Map(user);
                string authToken = tokenHandler.IssueToken(user);

                return new UserLogin(userView, authToken);
            } else {
                return null;
            }
        }
        #endregion
    }
}