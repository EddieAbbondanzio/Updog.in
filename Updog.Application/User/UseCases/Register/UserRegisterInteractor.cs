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
        private IDatabase database;
        private IUserViewMapper userMapper;
        private IPasswordHasher passwordHasher;
        private IAuthenticationTokenHandler tokenHandler;
        private AbstractValidator<UserRegisterParams> validator;
        #endregion

        #region Constructor(s)
        public UserRegisterInteractor(IDatabase database, IUserViewMapper userMapper, IPasswordHasher passwordHasher, IAuthenticationTokenHandler tokenHandler, AbstractValidator<UserRegisterParams> validator) {
            this.database = database;
            this.userMapper = userMapper;
            this.passwordHasher = passwordHasher;
            this.tokenHandler = tokenHandler;
            this.validator = validator;
        }
        #endregion

        #region Publics
        public async Task<UserLogin> Handle(UserRegisterParams input) {
            await validator.ValidateAndThrowAsync(input);

            using (var connection = database.GetConnection()) {
                IUserRepo userRepo = database.GetRepo<IUserRepo>(connection);

                // Check that the email is free first.
                User? emailInUse = await userRepo.FindByEmail(input.Email);
                if (emailInUse != null) {
                    throw new CollisionException("Email is already in use");
                }

                User? usernameInUse = await userRepo.FindByUsername(input.Username);
                if (usernameInUse != null) {
                    throw new CollisionException("Username is unavailable");
                }

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
        }
        #endregion
    }
}