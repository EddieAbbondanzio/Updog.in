
using System.Threading.Tasks;
using FluentValidation;

namespace Blurtle.Application {
    public sealed class UpdateUserInteractor : IRequestHandler<UpdateUserRequest> {
        #region Fields
        private IUserRepo userRepo;

        private AbstractValidator<UpdateUserRequest> userValidator;
        #endregion

        #region Constructor(s)
        public UpdateUserInteractor(IUserRepo userRepo, AbstractValidator<UpdateUserRequest> userValidator) {
            this.userRepo = userRepo;
            this.userValidator = userValidator;
        }
        #endregion

        #region Publics
        public async Task Handle(UpdateUserRequest input) {
            await this.userValidator.ValidateAndThrowAsync(input);

            input.User.Email = input.Email;
            await userRepo.Update(input.User);
        }
        #endregion
    }
}