using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnPostCommandHandler : CommandHandler<VoteOnPostCommand> {
        #region Fields
        private IVoteService service;
        #endregion

        #region Constructor(s)
        public VoteOnPostCommandHandler(IVoteService service) {
            this.service = service;
        }
        #endregion

        #region Private
        [Validate(typeof(VoteOnPostCommandValidator))]
        protected async override Task<CommandResult> ExecuteCommand(VoteOnPostCommand command) {
            Vote v = await service.VoteOnPost(new VoteOnPostData(command.PostId, command.Vote), command.User);
            return new InsertResult(true, v.Id);
        }
        #endregion
    }
}