using System.Threading.Tasks;
using FluentValidation;

namespace Blurtle.Application {
    public sealed class UserPasswordUpdater : IRequestHandler<UserPasswordUpdateRequest> {
        #region Fields
        private IUserRepo userRepo;

        private IPasswordHasher passwordHasher;

        private AbstractValidator<UserPasswordUpdateRequest> validator;
        #endregion

        #region Constructor(s)
        public UserPasswordUpdater(IUserRepo userRepo, IPasswordHasher passwordHasher, AbstractValidator<UserPasswordUpdateRequest> validator) {
            this.userRepo = userRepo;
            this.passwordHasher = passwordHasher;
            this.validator = validator;
        }
        #endregion

        public async Task Handle(UserPasswordUpdateRequest input) {
            await this.validator.ValidateAndThrowAsync(input);

            input.User.PasswordHash = passwordHasher.Hash(input.Password);
            await userRepo.Update(input.User);
        }
    }
}