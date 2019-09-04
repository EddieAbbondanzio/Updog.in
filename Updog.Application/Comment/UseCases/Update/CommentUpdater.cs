
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
        private IPermissionHandler<Comment> commentPermissionHandler;

        private ICommentRepo commentRepo;

        private AbstractValidator<CommentUpdateParams> commentValidator;

        /// <summary>
        /// Mapper to convert the comment into its DTO.
        /// </summary>
        private IMapper<Comment, CommentView> commentMapper;
        #endregion

        #region Constructor(s)
        public CommentUpdater(IPermissionHandler<Comment> commentPermissionHandler, ICommentRepo commentRepo, AbstractValidator<CommentUpdateParams> commentValidator, IMapper<Comment, CommentView> commentMapper) {
            this.commentPermissionHandler = commentPermissionHandler;
            this.commentRepo = commentRepo;
            this.commentValidator = commentValidator;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public async Task<CommentView> Handle(CommentUpdateParams input) {
            Comment comment = await commentRepo.FindById(input.CommentId);

            if (comment == null) {
                throw new NotFoundException();
            }

            if (!(await this.commentPermissionHandler.HasPermission(input.User, PermissionAction.UpdateComment, comment))) {
                throw new AuthorizationException();
            }

            await commentValidator.ValidateAndThrowAsync(input);

            comment.Body = input.Body;
            comment.WasUpdated = true;
            await commentRepo.Update(comment);

            return commentMapper.Map(comment);
        }
        #endregion
    }
}