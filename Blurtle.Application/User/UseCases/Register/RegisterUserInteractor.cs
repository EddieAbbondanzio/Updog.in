using System;
using System.Threading.Tasks;
using Blurtle.Domain;
using FluentValidation;

namespace Blurtle.Application {
    /// <summary>
    /// Use case interactor for registering a new user with the site.
    /// </summary>
    public sealed class RegisterUserInteractor : IRequestHandler<RegisterUserRequest, UserLogin> {
        #region Fields
        private IUserRepo userRepo;

        private IPasswordHasher passwordHasher;

        private IAuthenticationTokenHandler tokenHandler;

        private AbstractValidator<RegisterUserRequest> validator;
        #endregion

        #region Constructor(s)
        public RegisterUserInteractor(IUserRepo userRepo, IPasswordHasher passwordHasher, IAuthenticationTokenHandler tokenHandler, AbstractValidator<RegisterUserRequest> validator) {
            this.userRepo = userRepo;
            this.passwordHasher = passwordHasher;
            this.tokenHandler = tokenHandler;
            this.validator = validator;
        }
        #endregion

        #region Publics
        public async Task<UserLogin> Handle(RegisterUserRequest input) {
            await validator.ValidateAndThrowAsync(input);

            User user = new User() {
                Username = input.Username,
                PasswordHash = passwordHasher.Hash(input.Password),
                Email = input.Email,
                JoinedDate = System.DateTime.UtcNow
            };

            await userRepo.Add(user);
            string authToken = tokenHandler.IssueToken(user);

            return new UserLogin(new UserInfo(user.Username, user.JoinedDate), authToken);
        }
        #endregion
    }
}