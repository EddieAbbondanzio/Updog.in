using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnCommentCommandHandler : CommandHandler<VoteOnCommentCommand> {
        #region Fields
        private IVoteService service;
        #endregion

        #region Constructor(s)
        public VoteOnCommentCommandHandler(IVoteService service) {
            this.service = service;
        }
        #endregion

        #region Private
        [Validate(typeof(VoteOnCommentCommandValidator))]
        protected async override Task<Either<CommandResult, Error>> ExecuteCommand(VoteOnCommentCommand command) {
            Vote v = await service.VoteOnComment(command.Data, command.User);
            return Insert(v.Id);
        }
        #endregion
    }
}