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
        private IPostRepo _postRepo;

        private ICommentRepo _commentRepo;

        private IPermissionHandler<Comment> _permissionHandler;

        private AbstractValidator<CommentDeleteParams> _validator;

        /// <summary>
        /// Mapper to convert the comment into its DTO.
        /// </summary>
        private ICommentViewMapper _commentMapper;
        #endregion

        #region Constructor(s)
        public CommentDeleter(IPostRepo postRepo, ICommentRepo commentRepo, IPermissionHandler<Comment> permissionHandler, AbstractValidator<CommentDeleteParams> validator, ICommentViewMapper commentMapper) {
            _postRepo = postRepo;
            _commentRepo = commentRepo;
            _permissionHandler = permissionHandler;
            _validator = validator;
            _commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        public async Task<CommentView> Handle(CommentDeleteParams input) {
            throw new Exception();
            // Comment? c = await commentRepo.FindById(input.CommentId);
            Comment? c = null;

            if (c == null) {
                throw new InvalidOperationException();
            }

            if (!(await this._permissionHandler.HasPermission(input.User, PermissionAction.DeleteComment, c))) {
                throw new AuthorizationException();
            }

            await _validator.ValidateAndThrowAsync(input);

            c.WasDeleted = true;

            // using (var unitOfWork = CreateUnitOfWork()) {
            c.Post.CommentCount--;

            Task.WaitAll(
            // commentRepo.Update(c),
            // postRepo.Update(c.Post)
            );

            // }

            return _commentMapper.Map(c);
        }
        #endregion
    }
}