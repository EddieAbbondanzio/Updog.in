using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate that the update parameters of a post update are okay.
    /// </summary>
    public sealed class CommentUpdateValidator : AbstractValidator<CommentUpdateParams> {
        public CommentUpdateValidator() {
            RuleFor(p => p.User).NotNull();
            RuleFor(p => p.CommentId).NotEqual(0);
            RuleFor(update => update.Body).NotNull().NotEmpty().MaximumLength(Comment.BodyMaxLength);
        }
    }
}