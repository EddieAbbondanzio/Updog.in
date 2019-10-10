using Updog.Domain;
using FluentValidation;
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate that the update parameters of a post update are okay.
    /// </summary>
    internal sealed class PostUpdateCommandValidator : FluentValidatorAdapter<PostUpdateCommand> {
        public PostUpdateCommandValidator() {
            RuleFor(p => p.User).NotNull().WithMessage("User performing the action is null.");

            RuleFor(p => p.PostId).GreaterThan(0).WithMessage("Id to update is required.");

            RuleFor(p => p.Update.Body).NotNull().WithMessage("Body is required");
            RuleFor(p => p.Update.Body).NotEmpty().WithMessage("Body is required.");
            RuleFor(p => p.Update.Body).MaximumLength(Post.BodyMaxLength).WithMessage($"Body must be {Post.BodyMaxLength} characters or less.");
        }
    }
}