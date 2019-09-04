using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate new posts being created.
    /// </summary>
    public sealed class SpaceCreateValidator : AbstractValidator<SpaceCreateParams> {
        #region Constructor(s)
        public SpaceCreateValidator(ISpaceRepo spaceRepo) {
            RuleFor(pars => pars.Name).NotNull().NotEmpty().MaximumLength(Space.NameMaxLength).Matches(@"^[\w\s-]+$").MustAsync(async (name, cancellationToken) => {
                Space existingSpace = await spaceRepo.FindByName(name);
                return existingSpace == null;
            });
            RuleFor(pars => pars.Description).NotNull().MaximumLength(Space.DescriptionMaxLength);
            RuleFor(pars => pars.User).NotNull();
        }
        #endregion
    }
}