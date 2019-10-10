using FluentValidation;
using Updog.Application.Validation;
using Updog.Domain;

namespace Updog.Application {
    internal sealed class VoteOnPostCommandValidator : FluentValidatorAdapter<VoteOnPostCommand> {
        #region Constructor(s)
        public VoteOnPostCommandValidator() {
            RuleFor(p => p.Data.PostId).GreaterThan(0).WithMessage("Post Id is required.");
            RuleFor(p => p.Data.VoteDirection).IsInEnum().WithMessage("Vote direction must be 1, 0, or -1");
            RuleFor(p => p.User).NotNull().WithMessage("User is required.");
        }
        #endregion
    }
}