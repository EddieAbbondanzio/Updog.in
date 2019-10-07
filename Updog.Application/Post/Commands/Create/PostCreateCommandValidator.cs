using Updog.Domain;
using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate new posts being created.
    /// </summary>
    internal sealed class PostCreateCommandValidator : FluentValidatorAdapter<PostCreateCommand> {
        #region Constructor(s)
        public PostCreateCommandValidator() {
            RuleFor(p => p.CreationData.Type).IsInEnum().WithMessage("Type must be link, or text.");

            RuleFor(p => p.CreationData.Title).NotNull().WithMessage("Title is required.");
            RuleFor(p => p.CreationData.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(p => p.CreationData.Title).MaximumLength(Post.TitleMaxLength).WithMessage($"Title must be {Post.TitleMaxLength} characters or less.");

            RuleFor(p => p.CreationData.Body).NotNull().WithMessage("Body is required.");
            RuleFor(p => p.CreationData.Body).NotEmpty().WithMessage("Body is required.");
            RuleFor(p => p.CreationData.Body).MaximumLength(Post.BodyMaxLength).WithMessage($"Body must be {Post.BodyMaxLength} characters or less.");

            RuleFor(p => p.CreationData.SpaceId).NotNull().WithMessage("Space is required.");
            RuleFor(p => p.CreationData.SpaceId).NotEmpty().WithMessage("Space is required.");

            RuleFor(p => p.User).NotNull().WithMessage("User performing the action is null.");
        }
        #endregion
    }
}