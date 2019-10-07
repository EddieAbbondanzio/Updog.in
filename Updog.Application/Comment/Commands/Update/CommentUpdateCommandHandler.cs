using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentUpdateCommandHandler : CommandHandler<CommentUpdateCommand> {
        #region Fields
        private PermissionHandler<Comment> permissionHandler;
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentUpdateCommandHandler(IDatabase database, PermissionHandler<Comment> permissionHandler, ICommentViewMapper commentMapper) : base(database) {
            this.permissionHandler = permissionHandler;
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(CommentUpdateCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<CommentUpdateCommand> context) {
            ICommentRepo commentRepo = context.Database.GetRepo<ICommentRepo>();
            Comment? comment = await commentRepo.FindById(context.Input.CommentId);

            if (comment == null) {
                context.Output.NotFound();
                return;
            }

            if (!(await this.permissionHandler.HasPermission(context.Input.User, PermissionAction.UpdateComment, comment))) {
                context.Output.InvalidOperation();
                return;
            }

            comment.Body = context.Input.Body;

            await commentRepo.Update(comment);
            context.Output.Success(commentMapper.Map(comment));
        }
        #endregion
    }
}