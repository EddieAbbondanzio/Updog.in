using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentDeleteCommandHandler : CommandHandler<CommentDeleteCommand> {
        #region Fields
        private ICommentService service;
        #endregion

        #region Constructor(s)
        public CommentDeleteCommandHandler(ICommentService service) {
            this.service = service;
        }
        #endregion

        #region Publics
        [Policy(typeof(CommentAlterCommandPolicy))]
        [Validate(typeof(CommentDeleteCommandValidator))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(CommentDeleteCommand command) {
            if (!(await service.DoesCommentExist(command.CommentId))) {
                return new NotFoundError($"Comment {command.CommentId} does not exist.");
            }

            await service.Delete(command.CommentId, command.User);
            return Success();
        }
        #endregion
    }
}