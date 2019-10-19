using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentCreateCommandHandler : CommandHandler<CommentCreateCommand> {
        #region Fields
        private ICommentService service;
        #endregion

        #region Constructor(s)
        public CommentCreateCommandHandler(ICommentService service) {
            this.service = service;
        }
        #endregion

        #region Publics
        [Validate(typeof(CommentCreateCommandValidator))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(CommentCreateCommand command) {
            Comment c = await service.Create(command.Data, command.User);
            return Insert(c.Id);
        }
        #endregion
    }
}