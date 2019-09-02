using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    public sealed class UserPasswordUpdateValidator : AbstractValidator<UserPasswordUpdateParams> {
        public UserPasswordUpdateValidator() {
            RuleFor(reg => reg.Password).NotNull().NotEmpty().MinimumLength(User.PasswordMinLength);
        }
    }
}