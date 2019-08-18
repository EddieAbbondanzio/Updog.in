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
        private IUserRepo userRepo;

        private IMapper<User, UserView> userMapper;

        private IPasswordHasher passwordHasher;

        private IAuthenticationTokenHandler tokenHandler;

        private AbstractValidator<UserRegisterParams> validator;
        #endregion

        #region Constructor(s)
        public UserRegisterInteractor(IUserRepo userRepo, IMapper<User, UserView> userMapper, IPasswordHasher passwordHasher, IAuthenticationTokenHandler tokenHandler, AbstractValidator<UserRegisterParams> validator) {
            this.userRepo = userRepo;
            this.userMapper = userMapper;
            this.passwordHasher = passwordHasher;
            this.tokenHandler = tokenHandler;
            this.validator = validator;
        }
        #endregion

        #region Publics
        public async Task<UserLogin> Handle(UserRegisterParams input) {
            await validator.ValidateAndThrowAsync(input);

            User user = new User() {
                Username = input.Username,
                PasswordHash = passwordHasher.Hash(input.Password),
                Email = input.Email,
                JoinedDate = System.DateTime.UtcNow
            };

            await userRepo.Add(user);
            UserView userView = userMapper.Map(user);
            string authToken = tokenHandler.IssueToken(user);

            return new UserLogin(userView, authToken);

        }
        #endregion
    }
}