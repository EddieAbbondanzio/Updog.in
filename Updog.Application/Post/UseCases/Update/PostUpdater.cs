
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
        private IDatabase database;
        private IPermissionHandler<Post> postPermissionHandler;
        private AbstractValidator<PostUpdateParams> postValidator;
        private IPostViewMapper postMapper;
        #endregion

        #region Constructor(s)
        public PostUpdater(IDatabase database, IPermissionHandler<Post> postPermissionHandler, AbstractValidator<PostUpdateParams> postValidator, IPostViewMapper postMapper) {
            this.database = database;
            this.postPermissionHandler = postPermissionHandler;
            this.postValidator = postValidator;
            this.postMapper = postMapper;
        }
        #endregion

        #region Publics
        public async Task<PostView> Handle(PostUpdateParams input) {
            await postValidator.ValidateAndThrowAsync(input);

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