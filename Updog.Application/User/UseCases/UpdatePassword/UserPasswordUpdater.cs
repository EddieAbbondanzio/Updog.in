using System.Threading.Tasks;
using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    public sealed class UserPasswordUpdater : IInteractor<UserPasswordUpdateParams> {
        #region Fields
        private IUserRepo userRepo;

        private IPasswordHasher passwordHasher;

        private AbstractValidator<UserPasswordUpdateParams> validator;
        #endregion

        #region Constructor(s)
        public UserPasswordUpdater(IUserRepo userRepo, IPasswordHasher passwordHasher, AbstractValidator<UserPasswordUpdateParams> validator) {
            this.userRepo = userRepo;
            this.passwordHasher = passwordHasher;
            this.validator = validator;
        }
        #endregion

        public async Task Handle(UserPasswordUpdateParams input) {
            await this.validator.ValidateAndThrowAsync(input);

            //Verify the old password is a match first
            bool isMatch = passwordHasher.Verify(input.CurrentPassword, input.User.PasswordHash);

            if (isMatch) {
                input.User.PasswordHash = passwordHasher.Hash(input.NewPassword);
                await userRepo.Update(input.User);
            } else {
                throw new AuthorizationException();
            }
        }
    }
}