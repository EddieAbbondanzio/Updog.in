using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class VoteOnCommentCommandHandler : CommandHandler<VoteOnCommentCommand> {
        #region Fields
        private IVoteFactory voteFactory;
        #endregion

        #region Constructor(s)
        public VoteOnCommentCommandHandler(IVoteFactory voteFactory, IDatabase database) : base(database) {
            this.voteFactory = voteFactory;
        }
        #endregion

        #region Private
        [Validate(typeof(VoteOnCommentCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<VoteOnCommentCommand> context) {
            IVoteRepo voteRepo = context.Database.GetRepo<IVoteRepo>();
            ICommentRepo commentRepo = context.Database.GetRepo<ICommentRepo>();

            // Pull in the comment.
            Comment? comment = await commentRepo.FindById(context.Input.CommentId);

            if (comment == null) {
                context.Output.BadInput($"No comment with Id: {context.Input.CommentId} exist.");
                return;
            }

            Vote newVote = voteFactory.ForComment(context.Input.User, context.Input.CommentId, context.Input.Vote);
            comment.AddVote(newVote.Direction);

            await voteRepo.Add(newVote);
            await commentRepo.Update(comment);
        }
        #endregion
    }
}