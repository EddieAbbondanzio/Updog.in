
using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Updater to handler updating comments.
    /// </summary>
    public sealed class CommentUpdater : Interactor<CommentUpdateParams, CommentView> {
        #region Fields
        private IDatabase database;
        private IPermissionHandler<Comment> commentPermissionHandler;
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentUpdater(IDatabase database, IPermissionHandler<Comment> commentPermissionHandler, ICommentViewMapper commentMapper) {
            this.database = database;
            this.commentPermissionHandler = commentPermissionHandler;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(CommentUpdateValidator))]
        protected override async Task<CommentView> HandleInput(CommentUpdateParams input) {
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