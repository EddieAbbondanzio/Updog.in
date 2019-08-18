using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;

namespace Updog.Application {
    /// <summary>
    /// Interactor to handle deleting posts.
    /// </summary>
    public sealed class PostDeleter : IInteractor<PostDeleteParams, PostView> {
        #region Fields
        private IPermissionHandler<Post> permissionHandler;

        private IPostRepo postRepo;

        private AbstractValidator<PostDeleteParams> postValidator;

        private IMapper<Post, PostView> postMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post deleter.
        /// </summary>
        /// <param name="postPermissionHandler">Permissions handler for posts.</param>
        /// <param name="postRepo">CRUD interface for the posts in the DB.</param>
        /// <param name="postValidator">Validator for data.</param>
        /// <param name="postMapper">Mapper to convert post to DTO.</param>
        public PostDeleter(IPermissionHandler<Post> postPermissionHandler, IPostRepo postRepo, AbstractValidator<PostDeleteParams> postValidator, IMapper<Post, PostView> postMapper) {
            this.permissionHandler = postPermissionHandler;
            this.postRepo = postRepo;
            this.postValidator = postValidator;
            this.postMapper = postMapper;
        }
        #endregion

        #region Publics
        public async Task<PostView> Handle(PostDeleteParams input) {
            Post p = await postRepo.FindById(input.PostId);

            if (p == null) {
                throw new NotFoundException();
            }

            if (!(await this.permissionHandler.HasPermission(input.User, PermissionAction.DeletePost, p))) {
                throw new AuthorizationException();
            }

            await postValidator.ValidateAndThrowAsync(input);

            p.WasDeleted = true;
            await postRepo.Update(p);

            return postMapper.Map(p);
        }
        #endregion
    }
}