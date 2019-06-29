using System;
using Blurtle.Domain;
using FluentValidation;
using FluentValidation.Results;

namespace Blurtle.Application {
    /// <summary>
    /// Validator to validate user registrations.
    /// </summary>
    public sealed class UserRegistrationValidator : AbstractValidator<UserRegistration> {
        #region Constructor(s)
        public UserRegistrationValidator(IUserRepo userRepo) {
            RuleFor(reg => reg.Username).MinimumLength(4).MaximumLength(24).Matches(@"^[\w\s-]+$").MustAsync(async (username, cancellationToken) => {
                User existingUser = await userRepo.FindByUsername(username);
                return existingUser == null;
            });
            RuleFor(reg => reg.Password).NotNull().NotEmpty().MinimumLength(8);
            RuleFor(reg => reg.Email).EmailAddress().MaximumLength(64).MustAsync(async (email, cancellationToken) => {
                User existingUser = await userRepo.FindByEmail(email);
                return existingUser == null;
            }).When(reg => reg.Email != null);
        }
        #endregion

        #region Publics
        public override ValidationResult Validate(ValidationContext<UserRegistration> context) => throw new InvalidOperationException("This validator must be ran via ValidateAsync()");
        #endregion
    }
}