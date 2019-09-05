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
        private IPostRepo _postRepo;

        private ISpaceRepo _spaceRepo;

        private AbstractValidator<PostCreateParams> _postValidator;

        private IPostViewMapper _postMapper;
        #endregion

        #region Constructor(s)
        public PostCreator(IPostRepo postRepo, ISpaceRepo spaceRepo, AbstractValidator<PostCreateParams> postValidator, IPostViewMapper postMapper) {
            _postRepo = postRepo;
            _spaceRepo = spaceRepo;
            _postValidator = postValidator;
            _postMapper = postMapper;
        }
        #endregion

        #region Publics
        public async Task<PostView> Handle(PostCreateParams input) {
            await _postValidator.ValidateAndThrowAsync(input);

            Space s = await _spaceRepo.FindByName(input.Space);

            if (s == null) {
                throw new NotFoundException($"No space with name ${input.Space} found.");
            }

            Post post = new Post() {
                Type = input.Type,
                Title = input.Title,
                Body = input.Body,
                User = input.User,
                CreationDate = DateTime.UtcNow,
                Space = s
            };

            await _postRepo.Add(post);
            return _postMapper.Map(post);
        }
        #endregion
    }
}