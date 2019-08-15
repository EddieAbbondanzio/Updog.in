using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate new comments being created.
    /// </summary>
    public sealed class CommentCreateValidator : AbstractValidator<CommentCreateParams> {
        #region Constructor(s)
        public CommentCreateValidator() {
            RuleFor(reg => reg.Body).NotNull().NotEmpty().MaximumLength(Comment.BodyMaxLength);
        }
        #endregion
    }
}