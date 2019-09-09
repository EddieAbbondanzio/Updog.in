
using System;
using System.Threading.Tasks;
using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to update a user.
    /// </summary>
    public sealed class UserUpdater : IInteractor<UpdateUserParams> {
        #region Fields
        private IDatabase database;
        private AbstractValidator<UpdateUserParams> validator;
        #endregion

        #region Constructor(s)
        public UserUpdater(IDatabase database, AbstractValidator<UpdateUserParams> userValidator) {
            this.database = database;
            validator = userValidator;
        }
        #endregion

        #region Publics
        public async Task Handle(UpdateUserParams input) {
            await validator.ValidateAndThrowAsync(input);

            using (var connection = database.GetConnection()) {
                IUserRepo userRepo = database.GetRepo<IUserRepo>(connection);

                //Is the email already in use?
                User? existing = await userRepo.FindByEmail(input.Email);

                if (!existing?.Equals(input.User) ?? false) {
                    throw new CollisionException("Email is already in use.");
                }

                // Good to go, update and save off the change.
                input.User.Email = input.Email;
                await userRepo.Update(input.User);
            }
        }
        #endregion
    }
}