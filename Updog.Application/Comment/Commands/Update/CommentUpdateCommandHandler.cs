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
        protected async override Task<CommandResult> ExecuteCommand(CommentUpdateCommand command) {
            Comment c = await service.Update(command.Data, command.User);
            return new CommandResult(true);
        }
        #endregion
    }
}