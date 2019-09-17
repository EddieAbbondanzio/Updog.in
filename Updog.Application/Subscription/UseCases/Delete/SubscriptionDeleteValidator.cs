using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    internal sealed class SubscriptionDeleteValidator : FluentValidatorAdapter<SubscriptionCreateParams> {
        public SubscriptionDeleteValidator() {
            RuleFor(p => p.Space).NotNull().WithMessage("Space name is required.");
            RuleFor(p => p.Space).NotEmpty().WithMessage("Space name is required.");

            RuleFor(p => p.User).NotNull().WithMessage("User performing delete is required.");
        }
    }
}