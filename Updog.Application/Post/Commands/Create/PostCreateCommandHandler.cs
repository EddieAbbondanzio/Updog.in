using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostCreateCommandHandler : CommandHandler<PostCreateCommand> {
        #region Fields
        private ISpaceService spaceService;
        private IPostService postService;
        #endregion

        #region Constructor(s)
        public PostCreateCommandHandler(ISpaceService spaceService, IPostService postService) {
            this.spaceService = spaceService;
            this.postService = postService;
        }
        #endregion

        #region Publics
        [Validate(typeof(PostCreateCommandValidator))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(PostCreateCommand command) {
            Space? space = await spaceService.FindByName(command.Space);

            if (space == null) {
                return new NotFoundError($"Space {command.Space} does not exist.");
            }

            Post p = await postService.Create(command.Data, space, command.User);
            return Insert(p.Id);
        }
        #endregion
    }
}