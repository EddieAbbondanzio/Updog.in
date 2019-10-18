using FluentValidation;
using Updog.Application;

namespace Updog.Application {
    /// <summary>
    /// Validator to check that a comment can be deleted.
    /// </summary>
    public sealed class CommentDeleteCommandValidator : FluentValidatorAdapter<CommentDeleteCommand> {
        #region Constructor(s)
        public CommentDeleteCommandValidator() {
            RuleFor(c => c.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(c => c.CommentId).GreaterThan(0).WithMessage("Id of comment to delete is required.");
        }
        #endregion
    }

}