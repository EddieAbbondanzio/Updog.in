using System;
using Blurtle.Domain;
using FluentValidation;
using FluentValidation.Results;

namespace Blurtle.Application {
    /// <summary>
    /// Validator to validate user registrations.
    /// </summary>
    internal sealed class UserRegistrationValidator : AbstractValidator<UserRegistration> {
        #region Constructor(s)
        public UserRegistrationValidator(IUserRepo userRepo) {
            RuleFor(reg => reg.Username).MinimumLength(4).MaximumLength(24).Matches("").MustAsync(async (username, cancellationToken) => {
                User existingUser = await userRepo.FindByUsername(username);
                return existingUser != null;
            });
            RuleFor(reg => reg.Password).NotNull().MinimumLength(8);
            RuleFor(reg => reg.Email).EmailAddress().MaximumLength(64).When(reg => reg.Email != null);
        }
        #endregion

        #region Publics
        public override ValidationResult Validate(ValidationContext<UserRegistration> context) => throw new InvalidOperationException("This validator must be ran via ValidateAsync()");
        #endregion
    }
}