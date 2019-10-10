using Updog.Domain;
using FluentValidation;
using Updog.Application;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate that the update parameters of a space update are okay.
    /// </summary>
    internal sealed class SpaceUpdateCommandValidator : FluentValidatorAdapter<SpaceUpdateCommand> {
        public SpaceUpdateCommandValidator() {
            RuleFor(s => s.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(s => s.Update.Description).NotNull().WithMessage("Description is required.");
            RuleFor(s => s.Update.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(s => s.Update.Description).MaximumLength(Space.DescriptionMaxLength).WithMessage($"Description must be {Space.DescriptionMaxLength} characters or less.");
        }
    }
}