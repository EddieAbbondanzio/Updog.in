using FluentValidation;
using Updog.Application.Validation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate that a password is safe to update.
    /// </summary>
    internal sealed class UserUpdatePasswordCommandValidator : FluentValidatorAdapter<UserUpdatePasswordCommand> {
        public UserUpdatePasswordCommandValidator() {
            RuleFor(c => c.User).NotNull().WithMessage("User is required.");

            RuleFor(c => c.UpdatePassword.CurrentPassword).NotNull().WithMessage("Current password is required.");
            RuleFor(c => c.UpdatePassword.CurrentPassword).NotEmpty().WithMessage("Current password is required.");

            RuleFor(c => c.UpdatePassword.NewPassword).NotNull().WithMessage("Password is required.");
            RuleFor(c => c.UpdatePassword.NewPassword).NotEmpty().WithMessage("Password is required.");
            RuleFor(c => c.UpdatePassword.NewPassword).MinimumLength(User.PasswordMinLength).WithMessage($"Password must be at least {User.PasswordMinLength} characters.");
        }
    }
}