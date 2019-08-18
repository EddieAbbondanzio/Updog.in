
using System;
using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Updater to handler updating posts.
    /// </summary>
    public sealed class PostUpdater : IInteractor<PostUpdateParams, PostView> {
        #region Fields
        private IPermissionHandler<Post> postPermissionHandler;

        private IPostRepo postRepo;

        private AbstractValidator<PostUpdateParams> postValidator;

        private IMapper<Post, PostView> postMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post updater.
        /// </summary>
        /// <param name="postPermissionHandler">Permissions handler for posts.</param>
        /// <param name="postRepo">CRUD interface for posts.</param>
        /// <param name="postValidator">Data validator.</param>
        /// <param name="postMapper">Mapper to convert post to a post DTO.</param>
        public PostUpdater(IPermissionHandler<Post> postPermissionHandler, IPostRepo postRepo, AbstractValidator<PostUpdateParams> postValidator, IMapper<Post, PostView> postMapper) {
            this.postPermissionHandler = postPermissionHandler;
            this.postRepo = postRepo;
            this.postValidator = postValidator;
            this.postMapper = postMapper;
        }
        #endregion

        #region Publics
        public async Task<PostView> Handle(PostUpdateParams input) {
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

            return postMapper.Map(post);
        }
        #endregion
    }
}