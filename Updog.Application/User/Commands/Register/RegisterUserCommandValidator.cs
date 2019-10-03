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
    internal sealed class RegisterUserCommandValidator : FluentValidatorAdapter<RegisterUserCommand> {

        #region Constructor(s)
        public RegisterUserCommandValidator() {
            //Username
            RuleFor(cmd => cmd.Registration.Username).NotNull().WithMessage("Username is required.");
            RuleFor(cmd => cmd.Registration.Username).MinimumLength(User.UsernameMinLength).WithMessage($"Username must be at least {User.UsernameMinLength} characters.");
            RuleFor(cmd => cmd.Registration.Username).MaximumLength(User.UsernameMaxLength).WithMessage($"Username must be less than {User.UsernameMaxLength} characters.");
            RuleFor(cmd => cmd.Registration.Username).Matches(Regex.UrlSafe).WithMessage("Username may only contain letters, numbers, underscores, or hypens.");
            RuleFor(cmd => cmd.Registration.Username).Must((username) => !User.IsUsernameBanned(username)).WithMessage("Username is unavailable.");

            // Password
            RuleFor(cmd => cmd.Registration.Password).NotNull().WithMessage("Password is required.");
            RuleFor(cmd => cmd.Registration.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(cmd => cmd.Registration.Password).MinimumLength(User.PasswordMinLength).WithMessage($"Password must be at least {User.PasswordMinLength} characters.");

            // Email is only validated when provided.
            When(cmd => !String.IsNullOrWhiteSpace(cmd.Registration.Email), () => {
                RuleFor(cmd => cmd.Registration.Email).EmailAddress().WithMessage("Email must be valid.");
                RuleFor(cmd => cmd.Registration.Email).MaximumLength(User.EmailMaxLength).WithMessage($"Email must be less than {User.EmailMaxLength} characters.");
            });
        }
        #endregion
    }
}