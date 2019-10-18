using FluentValidation;

namespace Updog.Application {
    public sealed class RemoveAdminCommandValidator : FluentValidatorAdapter<RemoveAdminCommand> {
        public RemoveAdminCommandValidator() {
            RuleFor(c => c.Username).NotNull().WithMessage("Username is required.");
            RuleFor(c => c.Username).NotEmpty().WithMessage("Username is required.");
        }
    }
}