using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Interactor to handle deleting comments.
    /// </summary>
    public sealed class CommentDeleter : IInteractor<CommentDeleteParams, CommentView> {
        #region Fields
        private IPermissionHandler<Comment> permissionHandler;

        private ICommentRepo repo;

        private AbstractValidator<CommentDeleteParams> validator;

        /// <summary>
        /// Mapper to convert the comment into its DTO.
        /// </summary>
        private IMapper<Comment, CommentView> commentMapper;
        #endregion

        #region Constructor(s)
        public CommentDeleter(IPermissionHandler<Comment> permissionHandler, ICommentRepo repo, AbstractValidator<CommentDeleteParams> validator, IMapper<Comment, CommentView> commentMapper) {
            this.permissionHandler = permissionHandler;
            this.repo = repo;
            this.validator = validator;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public async Task<CommentView> Handle(CommentDeleteParams input) {
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

            return commentMapper.Map(c);
        }
        #endregion
    }
}