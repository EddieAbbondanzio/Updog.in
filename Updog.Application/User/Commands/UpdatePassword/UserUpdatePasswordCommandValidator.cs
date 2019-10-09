using FluentValidation;
using Updog.Application.Validation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate that a password is safe to update.
    /// </summary>
    internal sealed class UserUpdatePasswordCommandValidator : FluentValidatorAdapter<UserUpdatePasswordCommand> {
        public UserUpdatePasswordCommandValidator() {
            RuleFor(reg => reg.User).NotNull().WithMessage("User is required.");

            // RuleFor(reg => reg.CurrentPassword).NotNull().WithMessage("Current password is required.");
            // RuleFor(reg => reg.CurrentPassword).NotEmpty().WithMessage("Current password is required.");

            // RuleFor(reg => reg.NewPassword).NotNull().WithMessage("Password is required.");
            // RuleFor(reg => reg.NewPassword).NotEmpty().WithMessage("Password is required.");
            // RuleFor(reg => reg.NewPassword).MinimumLength(User.PasswordMinLength).WithMessage($"Password must be at least {User.PasswordMinLength} characters.");
        }
    }
}