using System;
using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Use case interactor for registering a new user with the site.
    /// </summary>
    public sealed class UserRegisterInteractor : IInteractor<UserRegisterParams, UserLogin> {
        #region Fields
        private IUserRepo _userRepo;

        private IMapper<User, UserView> _userMapper;

        private IPasswordHasher _passwordHasher;

        private IAuthenticationTokenHandler _tokenHandler;

        private AbstractValidator<UserRegisterParams> _validator;
        #endregion

        #region Constructor(s)
        public UserRegisterInteractor(IUserRepo userRepo, IMapper<User, UserView> userMapper, IPasswordHasher passwordHasher, IAuthenticationTokenHandler tokenHandler, AbstractValidator<UserRegisterParams> validator) {
            _userRepo = userRepo;
            _userMapper = userMapper;
            _passwordHasher = passwordHasher;
            _tokenHandler = tokenHandler;
            _validator = validator;
        }
        #endregion

        #region Publics
        public async Task<UserLogin> Handle(UserRegisterParams input) {
            await _validator.ValidateAndThrowAsync(input);

            // Check that the email is free first.
            User? existing = await _userRepo.FindByEmail(input.Email);
            if (existing != null) {
                throw new CollisionException("Email is already in use");
            }

            User user = new User() {
                Username = input.Username,
                PasswordHash = _passwordHasher.Hash(input.Password),
                Email = input.Email,
                JoinedDate = System.DateTime.UtcNow
            };

            await _userRepo.Add(user);
            UserView userView = _userMapper.Map(user);
            string authToken = _tokenHandler.IssueToken(user);

            return new UserLogin(userView, authToken);

        }
        #endregion
    }
}