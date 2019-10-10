using FluentValidation;
using Updog.Application;

namespace Updog.Application {
    /// <summary>
    /// Validator to find a resource via the user that created it..
    /// </summary>
    internal sealed class IsUsernameAvailableQueryValidator : FluentValidatorAdapter<IsUsernameAvailableQuery> {
        #region Constructor(s)
        public IsUsernameAvailableQueryValidator() {
            RuleFor(c => c.Username).NotNull().WithMessage("Username is required.");
            RuleFor(c => c.Username).NotEmpty().WithMessage("Username is required.");
        }
        #endregion
    }

}