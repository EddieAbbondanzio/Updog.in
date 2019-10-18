using Updog.Domain;
using FluentValidation;
using Updog.Application;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate that the update parameters of a post update are okay.
    /// </summary>
    public sealed class CommentUpdateCommandValidator : FluentValidatorAdapter<CommentUpdateCommand> {
        public CommentUpdateCommandValidator() {
            RuleFor(c => c.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(c => c.CommentId).GreaterThan(0).WithMessage("Id of comment to update is required.");

            RuleFor(c => c.Update.Body).NotNull().WithMessage("Body is required.");
            RuleFor(c => c.Update.Body).NotEmpty().WithMessage("Body is required.");
            RuleFor(c => c.Update.Body).MaximumLength(Comment.BodyMaxLength).WithMessage($"Body must be {Comment.BodyMaxLength} characters or less.");
        }
    }
}