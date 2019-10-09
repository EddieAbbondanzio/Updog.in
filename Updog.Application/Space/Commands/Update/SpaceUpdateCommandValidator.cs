using Updog.Domain;
using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate that the update parameters of a space update are okay.
    /// </summary>
    internal sealed class SpaceUpdateCommandValidator : FluentValidatorAdapter<SpaceUpdateCommand> {
        public SpaceUpdateCommandValidator() {
            RuleFor(s => s.User).NotNull().WithMessage("User performing the action is null.");

            // RuleFor(s => s.Description).NotNull().WithMessage("Description is required.");
            // RuleFor(s => s.Description).NotEmpty().WithMessage("Description is required.");
            // RuleFor(s => s.Description).MaximumLength(Space.DescriptionMaxLength).WithMessage($"Description must be {Space.DescriptionMaxLength} characters or less.");
        }
    }
}