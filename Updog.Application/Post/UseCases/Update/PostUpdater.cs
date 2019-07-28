
using System;
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
            Post post = await postRepo.FindById(input.PostId);

            if (post == null) {
                throw new NotFoundException();
            }

            if (post.Type == PostType.Link) {
                throw new InvalidOperationException("Link posts can't be updated.");
            }

            if (!(await this.postPermissionHandler.HasPermission(input.User, PermissionAction.UpdatePost, post))) {
                throw new AuthorizationException();
            }

            await postValidator.ValidateAndThrowAsync(input);

            post.Body = input.Body;
            post.WasUpdated = true;
            await postRepo.Update(post);
            return post;
        }
        #endregion
    }
}