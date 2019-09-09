using System.Threading.Tasks;
using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    public sealed class UserPasswordUpdater : IInteractor<UserPasswordUpdateParams> {
        #region Fields
        private IDatabase database;
        private IPasswordHasher passwordHasher;
        private AbstractValidator<UserPasswordUpdateParams> validator;
        #endregion

        #region Constructor(s)
        public UserPasswordUpdater(IDatabase database, IPasswordHasher passwordHasher, AbstractValidator<UserPasswordUpdateParams> validator) {
            this.database = database;
            this.passwordHasher = passwordHasher;
            this.validator = validator;
        }
        #endregion

        public async Task Handle(UserPasswordUpdateParams input) {
            await this.validator.ValidateAndThrowAsync(input);

            using (var connection = database.GetConnection()) {
                IUserRepo userRepo = database.GetRepo<IUserRepo>(connection);

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
}