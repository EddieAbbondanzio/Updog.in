
using System.Threading.Tasks;
using Blurtle.Domain;
using FluentValidation;

namespace Blurtle.Application {
    /// <summary>
    /// Updater to handler updating posts.
    /// </summary>
    public sealed class PostUpdater : IInteractor<PostUpdateParams, Post> {
        #region Fields
        private IPermissionHandler<Post> postPermissionHandler;

        private IPostRepo postRepo;

        private AbstractValidator<PostUpdateParams> postValidator;
        #endregion

        #region Constructor(s)
        public PostUpdater(IPermissionHandler<Post> postPermissionHandler, IPostRepo postRepo, AbstractValidator<PostUpdateParams> postValidator) {
            this.postPermissionHandler = postPermissionHandler;
            this.postRepo = postRepo;
            this.postValidator = postValidator;
        }
        #endregion

        #region Publics
        public async Task<Post> Handle(PostUpdateParams input) {
            if (!(await this.postPermissionHandler.HasPermission(input.User, PermissionAction.UpdatePost, input.Post))) {
                throw new AuthorizationException();
            }

            await postValidator.ValidateAndThrowAsync(input);

            input.Post.Body = input.Body;
            input.Post.WasUpdated = true;
            await postRepo.Update(input.Post);
            return input.Post;
        }
        #endregion
    }
}