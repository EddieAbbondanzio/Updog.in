
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
        private IDatabase database;
        private IPermissionHandler<Comment> commentPermissionHandler;
        private IValidator<CommentUpdateParams> commentValidator;
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentUpdater(IDatabase database, IPermissionHandler<Comment> commentPermissionHandler, IValidator<CommentUpdateParams> commentValidator, ICommentViewMapper commentMapper) {
            this.database = database;
            this.commentPermissionHandler = commentPermissionHandler;
            this.commentValidator = commentValidator;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public async Task<CommentView> Handle(CommentUpdateParams input) {
            await commentValidator.ValidateAndThrowAsync(input);

            using (var connection = database.GetConnection()) {
                ICommentRepo commentRepo = database.GetRepo<ICommentRepo>(connection);
                Comment? comment = await commentRepo.FindById(input.CommentId);

                if (comment == null) {
                    throw new NotFoundException();
                }

                if (!(await this.commentPermissionHandler.HasPermission(input.User, PermissionAction.UpdateComment, comment))) {
                    throw new AuthorizationException();
                }

                comment.Body = input.Body;
                comment.WasUpdated = true;

                await commentRepo.Update(comment);
                return commentMapper.Map(comment);
            }
        }
        #endregion
    }
}