using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Interactor to handle deleting comments.
    /// </summary>
    public sealed class CommentDeleter : IInteractor<CommentDeleteParams, Comment> {
        #region Fields
        private IPermissionHandler<Comment> permissionHandler;

        private ICommentRepo repo;

        private AbstractValidator<CommentDeleteParams> validator;
        #endregion

        #region Constructor(s)
        public CommentDeleter(IPermissionHandler<Comment> permissionHandler, ICommentRepo repo, AbstractValidator<CommentDeleteParams> validator) {
            this.permissionHandler = permissionHandler;
            this.repo = repo;
            this.validator = validator;
        }
        #endregion

        #region Publics
        public async Task<Comment> Handle(CommentDeleteParams input) {
            Comment c = await repo.FindById(input.CommentId);

            if (c == null) {
                throw new NotFoundException();
            }

            if (!(await this.permissionHandler.HasPermission(input.User, PermissionAction.DeleteComment, c))) {
                throw new AuthorizationException();
            }

            await validator.ValidateAndThrowAsync(input);

            c.WasDeleted = true;
            await repo.Update(c);
            return c;
        }
        #endregion
    }
}