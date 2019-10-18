using FluentValidation;
using Updog.Application;

namespace Updog.Application {
    public sealed class SubscriptionDeleteCommandValidator : FluentValidatorAdapter<SubscriptionDeleteCommand> {
        public SubscriptionDeleteCommandValidator() {
            RuleFor(p => p.Space).NotNull().WithMessage("Space name is required.");
            RuleFor(p => p.Space).NotEmpty().WithMessage("Space name is required.");

            RuleFor(p => p.User).NotNull().WithMessage("User performing delete is required.");
        }
    }
}