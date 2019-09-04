using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate when a user is updated.
    /// </summary>
    public sealed class UserUpdateValidator : AbstractValidator<UpdateUserParams> {
        public UserUpdateValidator() {
            RuleFor(reg => reg.Email).EmailAddress().WithMessage("Email must be valid.");
            RuleFor(reg => reg.Email).MaximumLength(User.EmailMaxLength).WithMessage($"Email must be less than {User.EmailMaxLength} characters.");
        }
    }
}