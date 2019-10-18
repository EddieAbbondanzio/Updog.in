using FluentValidation;
using Updog.Application;
using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnCommentCommandValidator : FluentValidatorAdapter<VoteOnCommentCommand> {
        #region Constructor(s)
        public VoteOnCommentCommandValidator() {
            RuleFor(p => p.Data.CommentId).GreaterThan(0).WithMessage("Comment Id is required.");
            RuleFor(p => p.Data.VoteDirection).IsInEnum().WithMessage("Vote direction must be 1, 0, or -1");
            RuleFor(p => p.User).NotNull().WithMessage("User is required.");
        }
        #endregion
    }
}