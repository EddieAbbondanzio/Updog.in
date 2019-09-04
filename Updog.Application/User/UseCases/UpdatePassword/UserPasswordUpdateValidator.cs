using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate that a password is safe to update.
    /// </summary>
    public sealed class UserPasswordUpdateValidator : AbstractValidator<UserPasswordUpdateParams> {
        public UserPasswordUpdateValidator() {
            RuleFor(reg => reg.Password).NotNull().WithMessage("Password is required.");
            RuleFor(reg => reg.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(reg => reg.Password).MinimumLength(User.PasswordMinLength).WithMessage($"Password must be at least {User.PasswordMinLength} characters.");
        }
    }
}