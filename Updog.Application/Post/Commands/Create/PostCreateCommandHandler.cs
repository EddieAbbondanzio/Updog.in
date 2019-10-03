using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostCreateCommandHandler : CommandHandler<PostCreateCommand> {
        #region Fields
        private IPostViewMapper postMapper;
        #endregion

        #region Constructor(s)
        public PostCreateCommandHandler(IDatabase database, IPostViewMapper postMapper) : base(database) {
            this.postMapper = postMapper;
        }
        #endregion

        #region Publics
        [Validate(typeof(PostCreateCommandValidator))]
        protected async override Task ExecuteCommand(ExecutionContext<PostCreateCommand> context) {
            ISpaceRepo spaceRepo = context.Database.GetRepo<ISpaceRepo>();
            IPostRepo postRepo = context.Database.GetRepo<IPostRepo>();
            IVoteRepo voteRepo = context.Database.GetRepo<IVoteRepo>();

            Space? space = await spaceRepo.FindByName(context.Input.Space);
            if (space == null) {
                context.Output.InvalidOperation($"No space with name ${context.Input.Space} found.");
                return;
            }

            Post post = new Post() {
                Type = context.Input.Type,
                Title = context.Input.Title,
                Body = context.Input.Body,
                User = context.Input.User,
                CreationDate = DateTime.UtcNow,
                Space = space
            };

            if (post.Type == PostType.Link && !System.Text.RegularExpressions.Regex.IsMatch(post.Body, Regex.UrlProtocol)) {
                post.Body = $"http://{post.Body}";
            }

            // Not liking these count caches. Makes no sense?
            post.Upvotes++;
            await postRepo.Add(post);

            Vote upvote = new Vote() {
                User = context.Input.User,
                ResourceId = post.Id,
                ResourceType = VotableEntityType.Post,
                Direction = VoteDirection.Up
            };

            await voteRepo.Add(upvote);
            post.Vote = upvote;

            context.Output.Success(postMapper.Map(post));
        }
        #endregion
    }
}