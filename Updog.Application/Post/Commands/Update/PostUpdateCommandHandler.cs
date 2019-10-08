using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostUpdateCommandHandler : CommandHandler<PostUpdateCommand> {
        #region Fields
        private PermissionHandler<Post> permissionHandler;
        #endregion

        #region Constructor(s)
        public PostUpdateCommandHandler(IDatabase database, IPostViewMapper postMapper, PermissionHandler<Post> permissionHandler) {
            this.postMapper = postMapper;
            this.permissionHandler = permissionHandler;
        }
        #endregion

        #region Publics
        [Validate(typeof(PostUpdateCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<PostUpdateCommand> context) {
            IPostRepo postRepo = context.Database.GetRepo<IPostRepo>();
            Post? post = await postRepo.FindById(context.Input.PostId);

            if (post == null) {
                context.Output.InvalidOperation();
                return;
            }

            if (!(await this.permissionHandler.HasPermission(context.Input.User, PermissionAction.UpdatePost, post))) {
                context.Output.Unauthorized();
                return;
            }

            if (post.Type == PostType.Link) {
                context.Output.InvalidOperation("Link posts can't be updated.");
                return;
            }

            if (post.WasDeleted) {
                context.Output.InvalidOperation("Post has already been deleted.");
                return;
            }


            post.Body = context.Input.Body;
            await postRepo.Update(post);

            context.Output.Success(postMapper.Map(post));
        }
        #endregion
    }
}