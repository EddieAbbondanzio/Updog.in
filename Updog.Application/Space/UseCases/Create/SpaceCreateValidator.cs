using Updog.Domain;
using FluentValidation;
using System.Linq;
using System;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate new posts being created.
    /// </summary>
    public sealed class SpaceCreateValidator : AbstractValidator<SpaceCreateParams> {
        #region Constructor(s)
        public SpaceCreateValidator() {
            RuleFor(s => s.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(s => s.Name).NotNull().WithMessage("Name is required.");
            RuleFor(s => s.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(s => s.Name).MaximumLength(Space.NameMaxLength).WithMessage($"Name must be {Space.NameMaxLength} characters or less.");
            RuleFor(s => s.Name).Matches(Regex.UrlSafe).WithMessage("Name may only contain letters, numbers, underscores, or hypens.");
            RuleFor(s => s.Name).Must((name) => Space.BannedNames.Any(s => String.Equals(name, s, StringComparison.OrdinalIgnoreCase))).WithMessage("Name is unavailable.");

            RuleFor(s => s.Description).NotNull().WithMessage("Description is required.");
            RuleFor(s => s.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(s => s.Description).MaximumLength(Space.DescriptionMaxLength).WithMessage($"Description must be {Space.DescriptionMaxLength} characters or less.");

        }
        #endregion
    }
}