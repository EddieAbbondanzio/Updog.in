using FluentValidation;

namespace Updog.Application {
    public sealed class UserPasswordValidator : AbstractValidator<UserPasswordUpdateParams> {
        public UserPasswordValidator() {
            RuleFor(reg => reg.Password).NotNull().NotEmpty().MinimumLength(8);
        }
    }
}