using System.Threading.Tasks;
using Blurtle.Domain;
using FluentValidation;

namespace Blurtle.Application {
    /// <summary>
    /// Interactor to handle deleting posts.
    /// </summary>
    public sealed class PostDeleter : IInteractor<PostDeleteParams, Post> {
        #region Fields
        private IPermissionHandler<Post> permissionHandler;

        private IPostRepo repo;

        private AbstractValidator<PostDeleteParams> validator;
        #endregion

        #region Constructor(s)
        public PostDeleter(IPermissionHandler<Post> permissionHandler, IPostRepo repo, AbstractValidator<PostDeleteParams> validator) {
            this.permissionHandler = permissionHandler;
            this.repo = repo;
            this.validator = validator;
        }
        #endregion

        #region Publics
        public async Task<Post> Handle(PostDeleteParams input) {
            if (!(await this.permissionHandler.HasPermission(input.User, PermissionAction.DeletePost, input.Post))) {
                throw new AuthorizationException();
            }

            await validator.ValidateAndThrowAsync(input);

            input.Post.WasDeleted = true;
            await repo.Update(input.Post);
            return input.Post;
        }
        #endregion
    }
}