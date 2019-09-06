
using System;
using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Updater to handler updating comments.
    /// </summary>
    public sealed class CommentUpdater : IInteractor<CommentUpdateParams, CommentView> {
        #region Fields
        private IPermissionHandler<Comment> _commentPermissionHandler;

        private ICommentRepo _commentRepo;

        private AbstractValidator<CommentUpdateParams> _commentValidator;

        /// <summary>
        /// Mapper to convert the comment into its DTO.
        /// </summary>
        private ICommentViewMapper _commentMapper;
        #endregion

        #region Constructor(s)
        public CommentUpdater(IPermissionHandler<Comment> commentPermissionHandler, ICommentRepo commentRepo, AbstractValidator<CommentUpdateParams> commentValidator, ICommentViewMapper commentMapper) {
            _commentPermissionHandler = commentPermissionHandler;
            _commentRepo = commentRepo;
            _commentValidator = commentValidator;
            _commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public async Task<CommentView> Handle(CommentUpdateParams input) {
            Comment? comment = await _commentRepo.FindById(input.CommentId);

            if (comment == null) {
                throw new NotFoundException();
            }

            if (!(await this._commentPermissionHandler.HasPermission(input.User, PermissionAction.UpdateComment, comment))) {
                throw new AuthorizationException();
            }

            await _commentValidator.ValidateAndThrowAsync(input);

            comment.Body = input.Body;
            comment.WasUpdated = true;
            await _commentRepo.Update(comment);

            return _commentMapper.Map(comment);
        }
        #endregion
    }
}