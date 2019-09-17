using FluentValidation;
using Updog.Application.Validation;
using Updog.Domain;

namespace Updog.Application {
    internal sealed class UserLoginValidator : FluentValidatorAdapter<UserCredentials> {
        public UserLoginValidator() {
            RuleFor(c => c.Username).NotNull().WithMessage("Username is required.");
            RuleFor(c => c.Username).NotEmpty().WithMessage("Username is required.");

            RuleFor(c => c.Password).NotNull().WithMessage("Password is required.");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}