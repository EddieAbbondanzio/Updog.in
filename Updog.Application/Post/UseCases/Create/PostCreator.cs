using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;
using System;

namespace Updog.Application {
    /// <summary>
    /// Adds new posts to the system.
    /// </summary>
    public sealed class PostCreator : IInteractor<PostCreateParams, PostView?> {
        #region Fields
        private IDatabase database;
        private AbstractValidator<PostCreateParams> postValidator;
        private IPostViewMapper postMapper;
        #endregion

        #region Constructor(s)
        public PostCreator(IDatabase database, AbstractValidator<PostCreateParams> postValidator, IPostViewMapper postMapper) {
            this.database = database;
            this.postValidator = postValidator;
            this.postMapper = postMapper;
        }
        #endregion

        #region Publics
        public async Task<PostView?> Handle(PostCreateParams input) {
            await postValidator.ValidateAndThrowAsync(input);

            using (var connection = database.GetConnection()) {
                ISpaceRepo spaceRepo = database.GetRepo<ISpaceRepo>(connection);
                IPostRepo postRepo = database.GetRepo<IPostRepo>(connection);
                IVoteRepo voteRepo = database.GetRepo<IVoteRepo>(connection);

                Space? space = await spaceRepo.FindByName(input.Space);
                if (space == null) {
                    throw new InvalidOperationException($"No space with name ${input.Space} found.");
                }

                using (var transaction = connection.BeginTransaction()) {
                    Post post = new Post() {
                        Type = input.Type,
                        Title = input.Title,
                        Body = input.Body,
                        User = input.User,
                        CreationDate = DateTime.UtcNow,
                        Space = space
                    };

                    // Not liking these count caches. Makes no sense?
                    post.Upvotes++;
                    await postRepo.Add(post);

                    Vote upvote = new Vote() {
                        User = input.User,
                        ResourceId = post.Id,
                        ResourceType = VoteResourceType.Post,
                        Direction = VoteDirection.Up
                    };

                    await voteRepo.Add(upvote);
                    post.Vote = upvote;

                    transaction.Commit();
                    return postMapper.Map(post);
                }
            }

        }
        #endregion
    }
}