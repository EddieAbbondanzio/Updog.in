using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    internal sealed class SubscriptionCreateValidator : FluentValidatorAdapter<SubscriptionCreateParams> {
        public SubscriptionCreateValidator() {
            RuleFor(p => p.Space).NotNull().WithMessage("Space name is required.");
            RuleFor(p => p.Space).NotEmpty().WithMessage("Space name is required.");

            RuleFor(p => p.User).NotNull().WithMessage("User creating subscription is required.");
        }
    }
}