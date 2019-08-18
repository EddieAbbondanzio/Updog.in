using System;
using Updog.Domain;
using FluentValidation;
using FluentValidation.Results;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate user registrations.
    /// </summary>
    public sealed class UserRegisterValidator : AbstractValidator<UserRegisterParams> {
        #region Constructor(s)
        public UserRegisterValidator(IUserRepo userRepo) {
            RuleFor(reg => reg.Username).NotNull().MinimumLength(4).MaximumLength(24).Matches(@"^[\w\s-]+$").MustAsync(async (username, cancellationToken) => {
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
        public override ValidationResult Validate(ValidationContext<UserRegisterParams> context) => throw new InvalidOperationException("This validator must be ran via ValidateAsync()");
        #endregion
    }
}