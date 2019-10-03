using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostDeleteCommandHandler : CommandHandler<PostDeleteCommand> {
        #region Fields
        private IPostViewMapper postMapper;

        private PermissionHandler<Post> permissionHandler;
        #endregion

        #region Constructor(s)
        public PostDeleteCommandHandler(IDatabase database, IPostViewMapper postMapper, PermissionHandler<Post> permissionHandler) : base(database) {
            this.postMapper = postMapper;
            this.permissionHandler = permissionHandler;
        }
        #endregion

        #region Publics
        [Validate(typeof(PostDeleteCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<PostDeleteCommand> context) {
            IPostRepo postRepo = context.Database.GetRepo<IPostRepo>();

            Post? p = await postRepo.FindById(context.Input.PostId);

            if (p == null) {
                context.Output.InvalidOperation();
                return;
            }

            if (!(await this.permissionHandler.HasPermission(context.Input.User, PermissionAction.DeletePost, p))) {
                context.Output.Unauthorized();
                return;
            }

            await postRepo.Delete(p);
            context.Output.Success(postMapper.Map(p));
        }
        #endregion
    }
}