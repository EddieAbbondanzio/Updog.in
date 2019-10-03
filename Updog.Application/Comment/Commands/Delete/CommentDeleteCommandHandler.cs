using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentDeleteCommandHandler : CommandHandler<CommentDeleteCommand> {
        #region Fields
        private PermissionHandler<Comment> permissionHandler;
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentDeleteCommandHandler(IDatabase database, PermissionHandler<Comment> permissionHandler, ICommentViewMapper commentMapper) : base(database) {
            this.permissionHandler = permissionHandler;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(CommentDeleteCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<CommentDeleteCommand> context) {
            ICommentRepo commentRepo = context.Database.GetRepo<ICommentRepo>();
            IPostRepo postRepo = context.Database.GetRepo<IPostRepo>();

            Comment? comment = await commentRepo.FindById(context.Input.CommentId);

            if (comment == null) {
                context.Output.InvalidOperation();
                return;
            }

            // Check to see if they have permission first.
            if (!(await this.permissionHandler.HasPermission(context.Input.User, PermissionAction.DeleteComment, comment))) {
                context.Output.InvalidOperation();
                return;
            }

            // (Hopefully) it would be impossible for post to be null if a comment exists...
            Post post = (await postRepo.FindById(comment.PostId))!;

            post.CommentCount -= (comment.ChildCount() + 1);

            await commentRepo.Delete(comment);
            await postRepo.Update(post);


            context.Output.Success(commentMapper.Map(comment));
        }
        #endregion
    }
}