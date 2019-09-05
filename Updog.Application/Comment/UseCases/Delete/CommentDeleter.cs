using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Interactor to handle deleting comments.
    /// </summary>
    public sealed class CommentDeleter : IInteractor<CommentDeleteParams, CommentView> {
        #region Fields
        private IPermissionHandler<Comment> _permissionHandler;

        private ICommentRepo _repo;

        private AbstractValidator<CommentDeleteParams> _validator;

        /// <summary>
        /// Mapper to convert the comment into its DTO.
        /// </summary>
        private ICommentViewMapper _commentMapper;
        #endregion

        #region Constructor(s)
        public CommentDeleter(IPermissionHandler<Comment> permissionHandler, ICommentRepo repo, AbstractValidator<CommentDeleteParams> validator, ICommentViewMapper commentMapper) {
            _permissionHandler = permissionHandler;
            _repo = repo;
            _validator = validator;
            _commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public async Task<CommentView> Handle(CommentDeleteParams input) {
            Comment c = await _repo.FindById(input.CommentId);

            if (c == null) {
                throw new NotFoundException();
            }

            if (!(await this._permissionHandler.HasPermission(input.User, PermissionAction.DeleteComment, c))) {
                throw new AuthorizationException();
            }

            await _validator.ValidateAndThrowAsync(input);

            c.WasDeleted = true;
            await _repo.Update(c);

            return _commentMapper.Map(c);
        }
        #endregion
    }
}