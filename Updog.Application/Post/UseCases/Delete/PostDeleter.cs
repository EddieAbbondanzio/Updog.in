using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;
using System;

namespace Updog.Application {
    /// <summary>
    /// Interactor to handle deleting posts.
    /// </summary>
    public sealed class PostDeleter : IInteractor<PostDeleteParams, PostView?> {
        #region Fields
        private IDatabase database;
        private IPermissionHandler<Post> permissionHandler;
        private IValidator<PostDeleteParams> postValidator;
        private IPostViewMapper postMapper;
        #endregion

        #region Constructor(s)
        public PostDeleter(IDatabase database, IPermissionHandler<Post> postPermissionHandler, IValidator<PostDeleteParams> postValidator, IPostViewMapper postMapper) {
            this.database = database;
            this.permissionHandler = postPermissionHandler;
            this.postValidator = postValidator;
            this.postMapper = postMapper;
        }
        #endregion

        #region Publics
        public async Task<PostView?> Handle(PostDeleteParams input) {
            await postValidator.ValidateAndThrowAsync(input);

            using (var connection = database.GetConnection()) {
                IPostRepo postRepo = database.GetRepo<IPostRepo>(connection);


                Post? p = await postRepo.FindById(input.PostId);

                if (p == null) {
                    throw new InvalidOperationException();
                }

                if (!(await this.permissionHandler.HasPermission(input.User, PermissionAction.DeletePost, p))) {
                    throw new AuthorizationException();
                }

                await postRepo.Delete(p);
                return postMapper.Map(p);
            }
        }
        #endregion
    }
}