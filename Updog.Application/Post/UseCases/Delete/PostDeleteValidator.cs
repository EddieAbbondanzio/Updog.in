using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Validator to check that a post can be deleted.
    /// </summary>
    internal sealed class PostDeleteValidator : FluentValidatorAdapter<PostDeleteParams> {
        #region Constructor(s)
        public PostDeleteValidator() {
            RuleFor(p => p.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(p => p.PostId).GreaterThan(0).WithMessage("Id to delete is required.");
        }
        #endregion
    }

}