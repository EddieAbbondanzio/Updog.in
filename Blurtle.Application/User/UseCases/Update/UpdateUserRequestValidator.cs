using Blurtle.Domain;
using FluentValidation;

namespace Blurtle.Application {
    /// <summary>
    /// Validator to validate when a user is updated.
    /// </summary>
    public sealed class UserUpdateRequestValidator : AbstractValidator<UpdateUserRequest> {
        public UserUpdateRequestValidator() {
            RuleFor(reg => reg.Email).EmailAddress().MaximumLength(64).When(reg => reg.Email != null);
        }
    }
}