
using System.Threading.Tasks;
using Blurtle.Domain;
using FluentValidation;

namespace Blurtle.Application {
    /// <summary>
    /// Updater to handler updating posts.
    /// </summary>
    public sealed class PostUpdater : IInteractor<PostUpdateParams, Post> {
        #region Fields
        private PostPermissionHandler postPermissionHandler;

        private IPostRepo postRepo;

        private PostUpdateValidator postValidator;
        #endregion

        #region Constructor(s)
        public PostUpdater(PostPermissionHandler postPermissionHandler, IPostRepo postRepo, PostUpdateValidator postValidator) {
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
            await postRepo.Update(input.Post);
            return input.Post;
        }
        #endregion
    }
}