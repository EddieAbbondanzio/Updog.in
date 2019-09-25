
using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Updater to handler updating posts.
    /// </summary>
    public sealed class PostUpdater : Interactor<PostUpdateParams, PostView> {
        #region Fields
        private IDatabase database;
        private PermissionHandler<Post> postPermissionHandler;
        private IPostViewMapper postMapper;
        #endregion

        #region Constructor(s)
        public PostUpdater(IDatabase database, PermissionHandler<Post> postPermissionHandler, IPostViewMapper postMapper) {
            this.database = database;
            this.postPermissionHandler = postPermissionHandler;
            this.postMapper = postMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(PostUpdateValidator))]
        protected override async Task<PostView> HandleInput(PostUpdateParams input) {
            using (var connection = database.GetConnection()) {
                IPostRepo postRepo = database.GetRepo<IPostRepo>(connection);
                Post? post = await postRepo.FindById(input.PostId);

                if (post == null) {
                    throw new InvalidOperationException();
                }

                if (!(await this.postPermissionHandler.HasPermission(input.User, PermissionAction.UpdatePost, post))) {
                    throw new AuthorizationException();
                }

                if (post.Type == PostType.Link) {
                    throw new InvalidOperationException("Link posts can't be updated.");
                }

                if (post.WasDeleted) {
                    throw new InvalidOperationException("Post has already been deleted.");
                }


                post.Body = input.Body;
                await postRepo.Update(post);

                return postMapper.Map(post);
            }
        }
        #endregion
    }
}