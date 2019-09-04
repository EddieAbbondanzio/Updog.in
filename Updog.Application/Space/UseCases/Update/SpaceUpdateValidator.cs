using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate that the update parameters of a space update are okay.
    /// </summary>
    public sealed class SpaceUpdateValidator : AbstractValidator<SpaceUpdateParams> {
        public SpaceUpdateValidator() {
            RuleFor(update => update.Description).NotNull().NotEmpty().MaximumLength(Space.DescriptionMaxLength);
        }
    }
}