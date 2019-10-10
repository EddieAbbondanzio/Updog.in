using FluentValidation;
using Updog.Application;

namespace Updog.Application {
    /// <summary>
    /// Validator to check that a post can be deleted.
    /// </summary>
    internal sealed class PostDeleteCommandValidator : FluentValidatorAdapter<PostDeleteCommand> {
        #region Constructor(s)
        public PostDeleteCommandValidator() {
            RuleFor(p => p.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(p => p.PostId).GreaterThan(0).WithMessage("Id to delete is required.");
        }
        #endregion
    }

}