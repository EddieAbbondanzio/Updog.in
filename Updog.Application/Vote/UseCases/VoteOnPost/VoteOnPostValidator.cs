using FluentValidation;
using Updog.Application.Validation;
using Updog.Domain;

namespace Updog.Application {
    internal sealed class VoteOnPostValidator : FluentValidatorAdapter<VoteOnPostParams> {
        #region Constructor(s)
        public VoteOnPostValidator() {
            RuleFor(p => p.PostId).GreaterThan(0).WithMessage("Post Id is required.");
            RuleFor(p => p.Vote).IsInEnum().WithMessage("Vote direction must be 1, 0, or -1");
            RuleFor(p => p.User).NotNull().WithMessage("User is required.");
        }
        #endregion
    }
}