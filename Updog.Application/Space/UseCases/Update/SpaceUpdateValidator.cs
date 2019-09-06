using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate that the update parameters of a space update are okay.
    /// </summary>
    public sealed class SpaceUpdateValidator : AbstractValidator<SpaceUpdateParams> {
        public SpaceUpdateValidator() {
            RuleFor(s => s.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(s => s.Description).NotNull().WithMessage("Description is required.");
            RuleFor(s => s.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(s => s.Description).MaximumLength(Space.DescriptionMaxLength).WithMessage($"Description must be {Space.DescriptionMaxLength} characters or less.");
        }
    }
}