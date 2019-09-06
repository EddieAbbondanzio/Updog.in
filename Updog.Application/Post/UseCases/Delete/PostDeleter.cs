using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Interactor to handle deleting posts.
    /// </summary>
    public sealed class PostDeleter : IInteractor<PostDeleteParams, PostView?> {
        #region Fields
        private IPermissionHandler<Post> _permissionHandler;

        private IPostRepo _postRepo;

        private AbstractValidator<PostDeleteParams> _postValidator;

        private IPostViewMapper _postMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post deleter.
        /// </summary>
        /// <param name="postPermissionHandler">Permissions handler for posts.</param>
        /// <param name="postRepo">CRUD interface for the posts in the DB.</param>
        /// <param name="postValidator">Validator for data.</param>
        /// <param name="postMapper">Mapper to convert post to DTO.</param>
        public PostDeleter(IPermissionHandler<Post> postPermissionHandler, IPostRepo postRepo, AbstractValidator<PostDeleteParams> postValidator, IPostViewMapper postMapper) {
            _permissionHandler = postPermissionHandler;
            _postRepo = postRepo;
            _postValidator = postValidator;
            _postMapper = postMapper;
        }
        #endregion

        #region Publics
        public async Task<PostView?> Handle(PostDeleteParams input) {
            Post? p = await _postRepo.FindById(input.PostId);

            if (p == null) {
                throw new NotFoundException();
            }

            if (!(await this._permissionHandler.HasPermission(input.User, PermissionAction.DeletePost, p))) {
                throw new AuthorizationException();
            }

            await _postValidator.ValidateAndThrowAsync(input);

            p.WasDeleted = true;
            await _postRepo.Update(p);

            return _postMapper.Map(p);
        }
        #endregion
    }
}