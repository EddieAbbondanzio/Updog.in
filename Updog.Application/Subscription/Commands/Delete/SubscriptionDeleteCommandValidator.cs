using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    internal sealed class SubscriptionDeleteCommandValidator : FluentValidatorAdapter<SubscriptionDeleteCommand> {
        public SubscriptionDeleteCommandValidator() {
            // RuleFor(p => p.Space).NotNull().WithMessage("Space name is required.");
            // RuleFor(p => p.Space).NotEmpty().WithMessage("Space name is required.");

            RuleFor(p => p.User).NotNull().WithMessage("User performing delete is required.");
        }
    }
}