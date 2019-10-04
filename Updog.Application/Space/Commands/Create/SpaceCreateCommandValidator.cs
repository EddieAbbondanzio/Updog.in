using Updog.Domain;
using FluentValidation;
using System.Linq;
using System;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate new posts being created.
    /// </summary>
    internal sealed class SpaceCreateCommandValidator : FluentValidatorAdapter<SpaceCreateCommand> {
        #region Constructor(s)
        public SpaceCreateCommandValidator() {
            RuleFor(s => s.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(s => s.CreationData.Name).NotNull().WithMessage("Name is required.");
            RuleFor(s => s.CreationData.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(s => s.CreationData.Name).MaximumLength(Space.NameMaxLength).WithMessage($"Name must be {Space.NameMaxLength} characters or less.");
            RuleFor(s => s.CreationData.Name).Matches(Regex.UrlSafe).WithMessage("Name may only contain letters, numbers, underscores, or hypens.");
            RuleFor(s => s.CreationData.Name).Must((name) => Space.BannedNames.Any(s => !String.Equals(name, s, StringComparison.OrdinalIgnoreCase))).WithMessage("Name is unavailable.");

            RuleFor(s => s.CreationData.Description).NotNull().WithMessage("Description is required.");
            RuleFor(s => s.CreationData.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(s => s.CreationData.Description).MaximumLength(Space.DescriptionMaxLength).WithMessage($"Description must be {Space.DescriptionMaxLength} characters or less.");

        }
        #endregion
    }
}