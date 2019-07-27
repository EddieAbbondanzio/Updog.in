using Blurtle.Domain;
using FluentValidation;

namespace Blurtle.Application {
    /// <summary>
    /// Validator to validate that the update parameters of a post update are okay.
    /// </summary>
    public sealed class PostUpdateValidator : AbstractValidator<PostUpdateParams> {
        public PostUpdateValidator() {
            RuleFor(update => update.Post.Type).Equal(PostType.Text);
            RuleFor(update => update.Body).NotNull().NotEmpty().MaximumLength(Post.BodyMaxLength);
        }
    }
}