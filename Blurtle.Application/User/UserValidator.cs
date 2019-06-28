using Blurtle.Domain;
using FluentValidation;

namespace Blurtle.Application {
    /// <summary>
    /// Validator to validate a user.
    /// </summary>
    public sealed class UserValidator : AbstractValidator<User> {
        #region Constants
        public const string RegistrationRuleSet = "registration";

        public const string UpdateRuleSet = "update";
        #endregion

        #region Constructor(s)
        public UserValidator(IUserRepo userRepo) {
            RuleSet(UserValidator.RegistrationRuleSet, () => {
                RuleFor(user => user.Username).NotNull().Length(4, 24).Matches("").MustAsync(async (username, cancelToken) => (await userRepo.FindByUsername(username)) == null).WithMessage("Username is unavailable");
                RuleFor(user => user.Email).EmailAddress().MaximumLength(64).When(user => user.Email != null);
                RuleFor(user => user.PasswordHash).NotNull();
            });

            RuleSet(UserValidator.UpdateRuleSet, () => {
                RuleFor(user => user.Email).EmailAddress().MaximumLength(64).When(user => user.Email != null);
                RuleFor(user => user.PasswordHash).NotNull();
            });
        }
        #endregion
    }
}