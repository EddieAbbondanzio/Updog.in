using Updog.Domain;
using FluentValidation;
using Updog.Application;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate when a user is updated.
    /// </summary>
    public sealed class UserUpdateCommandValidator : FluentValidatorAdapter<UserUpdateCommand> {
        public UserUpdateCommandValidator() {
            RuleFor(reg => reg.User).NotNull().WithMessage("User is required.");
            RuleFor(command => command.Update.Email).EmailAddress().WithMessage("Email must be valid.");
            RuleFor(command => command.Update.Email).MaximumLength(User.EmailMaxLength).WithMessage($"Email must be less than {User.EmailMaxLength} characters.");
        }
    }
}