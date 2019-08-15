using System.Threading.Tasks;
using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to create a new comment.
    /// </summary>
    public sealed class CommentCreator : IInteractor<CommentCreateParams, Comment> {
        #region Fields
        private ICommentRepo commentRepo;

        private AbstractValidator<CommentCreateParams> commentValidator;
        #endregion

        #region Constructor(s)
        public CommentCreator(ICommentRepo commentRepo, AbstractValidator<CommentCreateParams> commentValidator) {
            this.commentRepo = commentRepo;
            this.commentValidator = commentValidator;
        }
        #endregion

        #region Publics
        public async Task<Comment> Handle(CommentCreateParams input) {
            await commentValidator.ValidateAndThrowAsync(input);

            Comment comment = new Comment() {
                UserId = input.User.Id,
                PostId = input.PostId,
                ParentId = input.ParentId,
                Body = input.Body
            };

            await commentRepo.Add(comment);
            return comment;
        }
        #endregion
    }
}