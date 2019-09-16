using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;
using System;

namespace Updog.Application {
    /// <summary>
    /// Interactor to handle deleting comments.
    /// </summary>
    public sealed class CommentDeleter : IInteractor<CommentDeleteParams, CommentView> {
        #region Fields
        private IDatabase database;
        private IPermissionHandler<Comment> permissionHandler;
        private IValidator<CommentDeleteParams> validator;
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentDeleter(IDatabase database, IPermissionHandler<Comment> permissionHandler, AbstractValidator<CommentDeleteParams> validator, ICommentViewMapper commentMapper) {
            this.database = database;
            this.permissionHandler = permissionHandler;
            this.validator = validator;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public async Task<CommentView> Handle(CommentDeleteParams input) {
            await validator.ValidateAndThrowAsync(input);

            using (var connection = database.GetConnection()) {
                ICommentRepo commentRepo = database.GetRepo<ICommentRepo>(connection);
                IPostRepo postRepo = database.GetRepo<IPostRepo>(connection);

                Comment? comment = await commentRepo.FindById(input.CommentId);

                if (comment == null) {
                    throw new InvalidOperationException();
                }

                // Check to see if they have permission first.
                if (!(await this.permissionHandler.HasPermission(input.User, PermissionAction.DeleteComment, comment))) {
                    throw new AuthorizationException();
                }

                // (Hopefully) it would be impossible for post to be null if a comment exists...
                Post post = (await postRepo.FindById(comment.PostId))!;

                post.CommentCount--;

                using (var transaction = connection.BeginTransaction()) {
                    await commentRepo.Delete(comment);
                    await postRepo.Update(post);

                    transaction.Commit();
                }

                return commentMapper.Map(comment);
            }
        }
        #endregion
    }
}