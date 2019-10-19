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
        [Policy(typeof(PostAlterCommandPolicy))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(PostUpdateCommand command) {
            try {
                Post p = await service.Update(command.PostId, command.Update, command.User);
                return Success();
            } catch (NotFoundException e) {
                return Failure(e.Message);
            }
        }
        #endregion
    }
}