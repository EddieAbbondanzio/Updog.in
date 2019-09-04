using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to handler a login user use case.
    /// </summary>
    public sealed class UserLoginInteractor : IInteractor<UserLoginParams, UserLogin> {
        #region Fields
        private IUserRepo _userRepo;

        private IPasswordHasher _passwordHasher;

        private IAuthenticationTokenHandler _tokenHandler;

        private IMapper<User, UserView> _userMapper;
        #endregion

        #region Constructor(s)
        public UserLoginInteractor(IUserRepo userRepo, IMapper<User, UserView> userMapper, IPasswordHasher passwordHasher, IAuthenticationTokenHandler tokenHandler) {
            _userRepo = userRepo;
            _userMapper = userMapper;
            _passwordHasher = passwordHasher;
            _tokenHandler = tokenHandler;
        }
        #endregion

        #region Publics
        public async Task<UserLogin> Handle(UserLoginParams input) {
            User user = await _userRepo.FindByUsername(input.Username);

            if (user == null) {
                return null;
            }

            if (_passwordHasher.Verify(input.Password, user.PasswordHash)) {
                UserView userView = _userMapper.Map(user);
                string authToken = _tokenHandler.IssueToken(user);

                return new UserLogin(userView, authToken);
            } else {
                return null;
            }
        }
        #endregion
    }
}