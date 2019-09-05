using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate that the update parameters of a post update are okay.
    /// </summary>
    public sealed class PostUpdateValidator : AbstractValidator<PostUpdateParams> {
        public PostUpdateValidator() {
            RuleFor(p => p.PostId).GreaterThan(0).WithMessage("Id to update is required.");

            RuleFor(p => p.Body).NotNull().WithMessage("Body is required");
            RuleFor(p => p.Body).NotEmpty().WithMessage("Body is required.");
            RuleFor(p => p.Body).MaximumLength(Post.BodyMaxLength).WithMessage($"Body must be {Post.BodyMaxLength} characters or less.");
        }
    }
}