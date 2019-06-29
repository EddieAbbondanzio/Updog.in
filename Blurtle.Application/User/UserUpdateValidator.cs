using Blurtle.Domain;
using FluentValidation;

namespace Blurtle.Application {
    /// <summary>
    /// Validator to validate when a user is updated.
    /// </summary>
    public sealed class UserUpdateValidator : AbstractValidator<User> {
        public UserUpdateValidator() {
            RuleFor(reg => reg.Email).EmailAddress().MaximumLength(64).When(reg => reg.Email != null);
        }
    }
}