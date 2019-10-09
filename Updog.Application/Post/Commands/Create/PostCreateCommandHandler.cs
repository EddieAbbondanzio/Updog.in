using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostCreateCommandHandler : CommandHandler<PostCreateCommand> {
        #region Fields
        private IPostService service;
        #endregion

        #region Constructor(s)
        public PostCreateCommandHandler(IPostService service) {
            this.service = service;
        }
        #endregion

        #region Publics
        [Validate(typeof(PostCreateCommandValidator))]
        protected async override Task<CommandResult> ExecuteCommand(PostCreateCommand command) {
            Post p = await service.Create(command.Data, command.User);
            return new CommandResult(true, p.Id);
        }
        #endregion
    }
}