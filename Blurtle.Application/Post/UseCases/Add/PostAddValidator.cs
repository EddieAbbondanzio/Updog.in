using Blurtle.Domain;
using FluentValidation;

namespace Blurtle.Application {
    /// <summary>
    /// Validator to validate new posts being created.
    /// </summary>
    public sealed class PostAddValidator : AbstractValidator<PostAddParams> {
        #region Constructor(s)
        public PostAddValidator() {
            RuleFor(reg => reg.Title).NotNull().NotEmpty().MaximumLength(Post.TitleMaxLength);
            RuleFor(reg => reg.Body).NotNull().NotEmpty().MaximumLength(Post.BodyMaxLength);
        }
        #endregion
    }
}