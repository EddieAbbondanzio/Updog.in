using FluentValidation;

namespace Blurtle.Application {
    public sealed class UserPasswordUpdateValidator : AbstractValidator<UserPasswordUpdateRequest> {
        public UserPasswordUpdateValidator() {
            RuleFor(reg => reg.Password).NotNull().NotEmpty().MinimumLength(8);
        }
    }
}