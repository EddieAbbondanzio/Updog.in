using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnPostCommandHandler : CommandHandler<VoteOnPostCommand> {
        #region Fields
        private IVoteFactory voteFactory;
        #endregion

        #region Constructor(s)
        public VoteOnPostCommandHandler(IVoteFactory voteFactory, IDatabase database) : base(database) {
            this.voteFactory = voteFactory;
        }
        #endregion

        #region Private
        [Validate(typeof(VoteOnPostCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<VoteOnPostCommand> context) {
            IVoteRepo voteRepo = context.Database.GetRepo<IVoteRepo>();
            IPostRepo postRepo = context.Database.GetRepo<IPostRepo>();

            // Pull in the post.
            Post? post = await postRepo.FindById(context.Input.PostId);

            if (post == null) {
                context.Output.BadInput($"No post with Id: {context.Input.PostId} exist.");
                return;
            }

            Vote newVote = voteFactory.ForPost(context.Input.User, context.Input.PostId, context.Input.Vote);
            post.AddVote(newVote.Direction);

            await voteRepo.Add(newVote);
            await postRepo.Update(post);
        }
        #endregion
    }
}