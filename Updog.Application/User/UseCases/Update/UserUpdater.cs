
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
        private IUserRepo _repo;

        private AbstractValidator<UpdateUserParams> _validator;
        #endregion

        #region Constructor(s)
        public UserUpdater(IUserRepo userRepo, AbstractValidator<UpdateUserParams> userValidator) {
            _repo = userRepo;
            _validator = userValidator;
        }
        #endregion

        #region Publics
        public async Task Handle(UpdateUserParams input) {
            await _validator.ValidateAndThrowAsync(input);

            //Is the email already in use?
            User? existing = await _repo.FindByEmail(input.Email);

            if (!existing?.Equals(input.User) ?? false) {
                throw new CollisionException("Email is already in use.");
            }

            // Good to go, update and save off the change.
            input.User.Email = input.Email;
            await _repo.Update(input.User);
        }
        #endregion
    }
}