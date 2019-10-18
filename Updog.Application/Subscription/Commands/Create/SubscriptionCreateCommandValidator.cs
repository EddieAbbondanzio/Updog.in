using FluentValidation;
using Updog.Application;

namespace Updog.Application {
    public sealed class SubscriptionCreateCommandValidator : FluentValidatorAdapter<SubscriptionCreateCommand> {
        public SubscriptionCreateCommandValidator() {
            RuleFor(p => p.Data.Space).NotNull().WithMessage("Space name is required.");
            RuleFor(p => p.Data.Space).NotEmpty().WithMessage("Space name is required.");

            RuleFor(p => p.User).NotNull().WithMessage("User creating subscription is required.");
        }
    }
}