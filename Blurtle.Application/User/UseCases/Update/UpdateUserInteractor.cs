
using System.Threading.Tasks;
using FluentValidation;

namespace Blurtle.Application {
    public sealed class UpdateUserInteractor : IInteractor<UpdateUserParams> {
        #region Fields
        private IUserRepo userRepo;

        private AbstractValidator<UpdateUserParams> userValidator;
        #endregion

        #region Constructor(s)
        public UpdateUserInteractor(IUserRepo userRepo, AbstractValidator<UpdateUserParams> userValidator) {
            this.userRepo = userRepo;
            this.userValidator = userValidator;
        }
        #endregion

        #region Publics
        public async Task Handle(UpdateUserParams input) {
            await this.userValidator.ValidateAndThrowAsync(input);

            input.User.Email = input.Email;
            await userRepo.Update(input.User);
        }
        #endregion
    }
}