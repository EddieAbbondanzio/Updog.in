using Blurtle.Domain;
using FluentValidation;

namespace Blurtle.Application {
    public sealed class UserValidator : AbstractValidator<User> {
        public UserValidator() {
            RuleFor(reg => reg.Email).EmailAddress().MaximumLength(64).When(reg => reg.Email != null);
        }
    }
}