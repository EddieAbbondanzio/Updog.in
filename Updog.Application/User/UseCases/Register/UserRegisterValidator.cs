using System;
using Updog.Domain;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate user registrations.
    /// </summary>
    internal sealed class UserRegisterValidator : FluentValidatorAdapter<UserRegisterParams> {
        #region Constructor(s)
        public UserRegisterValidator() {
            //Username
            RuleFor(reg => reg.Username).NotNull().WithMessage("Username is required.");
            RuleFor(reg => reg.Username).MinimumLength(User.UsernameMinLength).WithMessage($"Username must be at least {User.UsernameMinLength} characters.");
            RuleFor(reg => reg.Username).MaximumLength(User.UsernameMaxLength).WithMessage($"Username must be less than {User.UsernameMaxLength} characters.");
            RuleFor(reg => reg.Username).Matches(Regex.UrlSafe).WithMessage("Username may only contain letters, numbers, underscores, or hypens.");
            RuleFor(reg => reg.Username).Must((username) => !User.BannedUsernames.Any(u => String.Equals(username, u, StringComparison.OrdinalIgnoreCase))).WithMessage("Username is unavailable.");

            // Password
            RuleFor(reg => reg.Password).NotNull().WithMessage("Password is required.");
            RuleFor(reg => reg.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(reg => reg.Password).MinimumLength(User.PasswordMinLength).WithMessage($"Password must be at least {User.PasswordMinLength} characters.");

            // Email is only validated when provided.
            When(reg => !String.IsNullOrWhiteSpace(reg.Email), () => {
                RuleFor(reg => reg.Email).EmailAddress().WithMessage("Email must be valid.");
                RuleFor(reg => reg.Email).MaximumLength(User.EmailMaxLength).WithMessage($"Email must be less than {User.EmailMaxLength} characters.");
            });
        }
        #endregion
    }
}