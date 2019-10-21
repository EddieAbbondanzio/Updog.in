using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentUpdateCommandHandler : CommandHandler<CommentUpdateCommand> {
        #region Fields
        private ICommentService service;
        #endregion

        #region Constructor(s)
        public CommentUpdateCommandHandler(ICommentService service) {
            this.service = service;
        }
        #endregion

        #region Publics
        [Validate(typeof(CommentUpdateCommandValidator))]
        [Policy(typeof(CommentAlterCommandPolicy))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(CommentUpdateCommand command) {
            if (!(await service.DoesCommentExist(command.CommentId))) {
                return new NotFoundError($"Comment {command.CommentId} does not exist.");
            }

            await service.Update(command.CommentId, command.Update, command.User);
            return Success();
        }
        #endregion
    }
}