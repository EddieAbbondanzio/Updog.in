using Updog.Domain;
using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate when a user is updated.
    /// </summary>
    internal sealed class UserUpdateCommandValidator : FluentValidatorAdapter<UserUpdateCommand> {
        public UserUpdateCommandValidator() {
            RuleFor(reg => reg.User).NotNull().WithMessage("User is required.");
            // RuleFor(reg => reg.Email).EmailAddress().WithMessage("Email must be valid.");
            // RuleFor(reg => reg.Email).MaximumLength(User.EmailMaxLength).WithMessage($"Email must be less than {User.EmailMaxLength} characters.");
        }
    }
}