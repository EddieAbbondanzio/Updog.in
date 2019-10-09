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
        [Validate(typeof(CommentDeleteCommandValidator))]
        protected async override Task<CommandResult> ExecuteCommand(CommentDeleteCommand command) {
            // (Hopefully) it would be impossible for post to be null if a comment exists...
            Comment c = await service.Delete(command.CommentId, command.User);
            return new CommandResult(true);
        }
        #endregion
    }
}