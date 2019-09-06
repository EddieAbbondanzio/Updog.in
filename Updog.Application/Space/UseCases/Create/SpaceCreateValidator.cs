using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate new posts being created.
    /// </summary>
    public sealed class SpaceCreateValidator : AbstractValidator<SpaceCreateParams> {
        #region Constructor(s)
        public SpaceCreateValidator(ISpaceRepo spaceRepo) {
            RuleFor(s => s.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(s => s.Name).NotNull().WithMessage("Name is required.");
            RuleFor(s => s.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(s => s.Name).MaximumLength(Space.NameMaxLength).WithMessage($"Name must be {Space.NameMaxLength} characters or less.");
            RuleFor(s => s.Name).Matches(Regex.UrlSafe).WithMessage("Name may only contain letters, numbers, underscores, or hypens.");

            RuleFor(s => s.Description).NotNull().WithMessage("Description is required.");
            RuleFor(s => s.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(s => s.Description).MaximumLength(Space.DescriptionMaxLength).WithMessage($"Description must be {Space.DescriptionMaxLength} characters or less.");

        }
        #endregion
    }
}