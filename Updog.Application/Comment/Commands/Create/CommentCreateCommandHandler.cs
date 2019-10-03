using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentCreateCommandHandler : CommandHandler<CommentCreateCommand> {
        #region Fields
        private ICommentViewMapper commentMapper;
        #endregion

        #region Constructor(s)
        public CommentCreateCommandHandler(IDatabase database, ICommentViewMapper commentMapper) : base(database) {
            this.commentMapper = commentMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(CommentCreateCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<CommentCreateCommand> context) {
            IPostRepo postRepo = context.Database.GetRepo<IPostRepo>();
            ICommentRepo commentRepo = context.Database.GetRepo<ICommentRepo>();
            IVoteRepo voteRepo = context.Database.GetRepo<IVoteRepo>();

            // Locate the post to ensure it actually exists.
            Post? post = await postRepo.FindById(context.Input.PostId);

            if (post == null) {
                context.Output.InvalidOperation();
                return;
            }

            Comment comment = new Comment() {
                User = context.Input.User,
                PostId = post.Id,
                Body = context.Input.Body,
                CreationDate = DateTime.UtcNow
            };

            // Set the parent comment if needed.
            if (context.Input.ParentId != 0) {
                comment.Parent = await commentRepo.FindById(context.Input.ParentId);
            }

            // Update the comment count cache on the post.
            post.CommentCount++;

            comment.Upvotes++;
            await commentRepo.Add(comment);

            Vote upvote = new Vote() {
                User = context.Input.User,
                ResourceId = comment.Id,
                ResourceType = VotableEntityType.Comment,
                Direction = VoteDirection.Up
            };

            await postRepo.Update(post);
            await voteRepo.Add(upvote);

            comment.Vote = upvote;

            context.Output.Success(commentMapper.Map(comment));
        }
        #endregion
    }
}