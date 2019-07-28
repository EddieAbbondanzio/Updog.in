using FluentValidation;

namespace Blurtle.Application {
    /// <summary>
    /// Validator to check that a post can be deleted.
    /// </summary>
    public sealed class PostDeleteValidator : AbstractValidator<PostDeleteParams> {
        #region Constructor(s)
        public PostDeleteValidator() {
            RuleFor(p => p.User).NotNull();
            RuleFor(p => p.PostId).NotEqual(0);
        }
        #endregion
    }

}