using Updog.Domain;
using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate new comments being created.
    /// </summary>
    internal sealed class CommentCreateValidator : FluentValidatorAdapter<CommentCreateParams> {
        #region Constructor(s)
        public CommentCreateValidator() {
            RuleFor(c => c.PostId).GreaterThan(0).WithMessage("Post Id is required.");

            RuleFor(c => c.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(c => c.Body).NotNull().WithMessage("Body is required.");

            RuleFor(c => c.Body).NotNull().WithMessage("Body is required.");
            RuleFor(c => c.Body).NotEmpty().WithMessage("Body is required.");
            RuleFor(c => c.Body).MaximumLength(Comment.BodyMaxLength).WithMessage($"Body must be {Comment.BodyMaxLength} characters or less.");
        }
        #endregion
    }
}