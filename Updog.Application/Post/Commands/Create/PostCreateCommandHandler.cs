using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostCreateCommandHandler : CommandHandler<PostCreateCommand> {
        #region Fields
        private ISpaceRepo repo;
        private IPostService service;
        #endregion

        #region Constructor(s)
        public PostCreateCommandHandler(ISpaceRepo repo, IPostService service) {
            this.repo = repo;
            this.service = service;
        }
        #endregion

        #region Publics
        [Validate(typeof(PostCreateCommandValidator))]
        protected async override Task<CommandResult> ExecuteCommand(PostCreateCommand command) {
            Space? s = await repo.FindByName(command.Space);

            if (s == null) {
                throw new NotFoundException($"Space with name: {command.Space} was not found.");
            }

            Post p = await service.Create(command.Data, s, command.User);
            return Success(p.Id);
        }
        #endregion
    }
}