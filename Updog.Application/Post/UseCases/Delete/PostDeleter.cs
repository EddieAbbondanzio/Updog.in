using System.Threading.Tasks;
using Updog.Domain;
using System;

namespace Updog.Application {
    /// <summary>
    /// Interactor to handle deleting posts.
    /// </summary>
    public sealed class PostDeleter : Interactor<PostDeleteParams, PostView?> {
        #region Fields
        private IDatabase database;
        private PermissionHandler<Post> permissionHandler;
        private IPostViewMapper postMapper;
        #endregion

        #region Constructor(s)
        public PostDeleter(IDatabase database, PermissionHandler<Post> postPermissionHandler, IPostViewMapper postMapper) {
            this.database = database;
            this.permissionHandler = postPermissionHandler;
            this.postMapper = postMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(PostDeleteValidator))]
        protected override async Task<PostView?> HandleInput(PostDeleteParams input) {
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