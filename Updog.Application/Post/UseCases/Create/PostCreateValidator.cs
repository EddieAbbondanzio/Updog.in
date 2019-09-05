using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate new posts being created.
    /// </summary>
    public sealed class PostCreateValidator : AbstractValidator<PostCreateParams> {
        #region Constructor(s)
        public PostCreateValidator() {
            RuleFor(p => p.Type).IsInEnum().WithMessage("Type must be link, or text.");

            RuleFor(p => p.Title).NotNull().WithMessage("Title is required.");
            RuleFor(p => p.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(p => p.Title).MaximumLength(Post.TitleMaxLength).WithMessage($"Title must be {Post.TitleMaxLength} characters or less.");

            RuleFor(p => p.Body).NotNull().WithMessage("Body is required.");
            RuleFor(p => p.Body).NotEmpty().WithMessage("Body is required.");
            RuleFor(p => p.Body).MaximumLength(Post.BodyMaxLength).WithMessage($"Body must be {Post.BodyMaxLength} characters or less.");

            RuleFor(p => p.Space).NotNull().WithMessage("Space is required.");
            RuleFor(p => p.Space).NotEmpty().WithMessage("Space is required.");
        }
        #endregion
    }
}