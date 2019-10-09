using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostDeleteCommandHandler : CommandHandler<PostDeleteCommand> {
        #region Fields
        private IPostService service;
        #endregion

        #region Constructor(s)
        public PostDeleteCommandHandler(IPostService service) {
            this.service = service;
        }
        #endregion

        #region Publics
        [Validate(typeof(PostDeleteCommandValidator))]
        protected async override Task<CommandResult> ExecuteCommand(PostDeleteCommand command) {
            Post p = await service.Delete(command.Data, command.User);
            return new CommandResult(true);
        }
        #endregion
    }
}