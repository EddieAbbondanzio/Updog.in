using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to check that a comment can be deleted.
    /// </summary>
    public sealed class CommentDeleteValidator : AbstractValidator<CommentDeleteParams> {
        #region Constructor(s)
        public CommentDeleteValidator() {
            RuleFor(p => p.User).NotNull();
            RuleFor(p => p.CommentId).NotEqual(0);
        }
        #endregion
    }

}