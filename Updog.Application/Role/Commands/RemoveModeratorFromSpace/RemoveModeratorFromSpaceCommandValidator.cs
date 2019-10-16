using FluentValidation;

namespace Updog.Application {
    internal sealed class RemoveModeratorFromSpaceCommandValidator : FluentValidatorAdapter<RemoveModeratorFromSpaceCommand> {
        public RemoveModeratorFromSpaceCommandValidator() {
            RuleFor(c => c.Username).NotNull().WithMessage("Username is required.");
            RuleFor(c => c.Username).NotEmpty().WithMessage("Username is required.");

            RuleFor(c => c.Space).NotNull().WithMessage("Space is required.");
            RuleFor(c => c.Space).NotEmpty().WithMessage("Space is required.");
        }
    }
}