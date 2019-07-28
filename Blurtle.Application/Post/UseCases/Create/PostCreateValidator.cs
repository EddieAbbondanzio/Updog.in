using Blurtle.Domain;
using FluentValidation;

namespace Blurtle.Application {
    /// <summary>
    /// Validator to validate new posts being created.
    /// </summary>
    public sealed class PostCreateValidator : AbstractValidator<PostCreateParams> {
        #region Constructor(s)
        public PostCreateValidator() {
            RuleFor(reg => reg.Title).NotNull().NotEmpty().MaximumLength(Post.TitleMaxLength);
            RuleFor(reg => reg.Body).NotNull().NotEmpty().MaximumLength(Post.BodyMaxLength);
        }
        #endregion
    }
}