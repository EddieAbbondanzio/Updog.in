using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to check that a post can be deleted.
    /// </summary>
    public sealed class PostDeleteValidator : AbstractValidator<PostDeleteParams> {
        #region Constructor(s)
        public PostDeleteValidator() {
            RuleFor(p => p.PostId).GreaterThan(0).WithMessage("Id to delete is required.");
        }
        #endregion
    }

}