using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostUpdateCommandHandler : CommandHandler<PostUpdateCommand> {
        #region Fields
        private IPostService service;
        #endregion

        #region Constructor(s)
        public PostUpdateCommandHandler(IPostService service) {
            this.service = service;
        }
        #endregion

        #region Publics
        [Validate(typeof(PostUpdateCommandValidator))]
        protected async override Task<CommandResult> ExecuteCommand(PostUpdateCommand command) {
            Post p = await service.Update(new PostUpdateData(command.PostId, command.Body), command.User);
            return new CommandResult(true);
        }
        #endregion
    }
}