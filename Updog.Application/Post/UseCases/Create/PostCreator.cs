using System.Threading.Tasks;
using Updog.Domain;
using FluentValidation;
using System;

namespace Updog.Application {
    /// <summary>
    /// Adds new posts to the system.
    /// </summary>
    public sealed class PostCreator : IInteractor<PostCreateParams, PostView> {
        #region Fields
        private IPostRepo postRepo;

        private AbstractValidator<PostCreateParams> postValidator;

        private IMapper<Post, PostView> postMapper;
        #endregion

        #region Constructor(s)
        public PostCreator(IPostRepo postRepo, AbstractValidator<PostCreateParams> postValidator, IMapper<Post, PostView> postMapper) {
            this.postRepo = postRepo;
            this.postValidator = postValidator;
            this.postMapper = postMapper;
        }
        #endregion

        #region Publics
        public async Task<PostView> Handle(PostCreateParams input) {
            await postValidator.ValidateAndThrowAsync(input);

            Post post = new Post() {
                Type = input.Type,
                Title = input.Title,
                Body = input.Body,
                User = input.User,
                CreationDate = DateTime.UtcNow
            };

            await postRepo.Add(post);
            return postMapper.Map(post);
        }
        #endregion
    }
}