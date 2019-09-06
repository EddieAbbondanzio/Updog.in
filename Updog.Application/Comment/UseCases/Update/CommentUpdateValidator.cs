using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate that the update parameters of a post update are okay.
    /// </summary>
    public sealed class CommentUpdateValidator : AbstractValidator<CommentUpdateParams> {
        public CommentUpdateValidator() {
            RuleFor(c => c.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(c => c.CommentId).GreaterThan(0).WithMessage("Id of comment to update is required.");

            RuleFor(c => c.Body).NotNull().WithMessage("Body is required.");
            RuleFor(c => c.Body).NotEmpty().WithMessage("Body is required.");
            RuleFor(c => c.Body).MaximumLength(Comment.BodyMaxLength).WithMessage($"Body must be {Comment.BodyMaxLength} characters or less.");
        }
    }
}