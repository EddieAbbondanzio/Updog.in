using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate when a user is updated.
    /// </summary>
    public sealed class UserUpdateValidator : AbstractValidator<UpdateUserParams> {
        public UserUpdateValidator() {
            RuleFor(reg => reg.Email).NotNull().EmailAddress().MaximumLength(64).When(reg => reg.Email != null);
        }
    }
}