using FluentValidation;

namespace Updog.Application {
    public sealed class AddModeratorToSpaceCommandValidator : FluentValidatorAdapter<AddModeratorToSpaceCommand> {
        public AddModeratorToSpaceCommandValidator() {
            RuleFor(c => c.Username).NotNull().WithMessage("Username is required.");
            RuleFor(c => c.Username).NotEmpty().WithMessage("Username is required.");

            RuleFor(c => c.Space).NotNull().WithMessage("Space is required.");
            RuleFor(c => c.Space).NotEmpty().WithMessage("Space is required.");
        }
    }
}