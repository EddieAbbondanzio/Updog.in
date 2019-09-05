
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
        private IPermissionHandler<Post> _postPermissionHandler;

        private IPostRepo _postRepo;

        private AbstractValidator<PostUpdateParams> _postValidator;

        private IPostViewMapper _postMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post updater.
        /// </summary>
        /// <param name="postPermissionHandler">Permissions handler for posts.</param>
        /// <param name="postRepo">CRUD interface for posts.</param>
        /// <param name="postValidator">Data validator.</param>
        /// <param name="postMapper">Mapper to convert post to a post DTO.</param>
        public PostUpdater(IPermissionHandler<Post> postPermissionHandler, IPostRepo postRepo, AbstractValidator<PostUpdateParams> postValidator, IPostViewMapper postMapper) {
            _postPermissionHandler = postPermissionHandler;
            _postRepo = postRepo;
            _postValidator = postValidator;
            _postMapper = postMapper;
        }
        #endregion

        #region Publics
        public async Task<PostView> Handle(PostUpdateParams input) {
            Post post = await _postRepo.FindById(input.PostId);

            if (post == null) {
                throw new NotFoundException();
            }

            if (!(await this._postPermissionHandler.HasPermission(input.User, PermissionAction.UpdatePost, post))) {
                throw new AuthorizationException();
            }

            if (post.Type == PostType.Link) {
                throw new InvalidOperationException("Link posts can't be updated.");
            }

            if (post.WasDeleted) {
                throw new InvalidOperationException("Post has already been deleted.");
            }

            await _postValidator.ValidateAndThrowAsync(input);

            post.Body = input.Body;
            post.WasUpdated = true;
            await _postRepo.Update(post);

            return _postMapper.Map(post);
        }
        #endregion
    }
}