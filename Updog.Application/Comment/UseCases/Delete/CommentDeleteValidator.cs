using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to check that a comment can be deleted.
    /// </summary>
    public sealed class CommentDeleteValidator : AbstractValidator<CommentDeleteParams> {
        #region Constructor(s)
        public CommentDeleteValidator() {
            RuleFor(c => c.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(c => c.CommentId).GreaterThan(0).WithMessage("Id of comment to delete is required.");
        }
        #endregion
    }

}