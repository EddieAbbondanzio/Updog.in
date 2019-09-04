using System.Threading.Tasks;
using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to create a new space.
    /// </summary>
    public sealed class SpaceCreator : IInteractor<SpaceCreateParams, SpaceView> {
        #region Fields
        private ISpaceRepo spaceRepo;

        private AbstractValidator<SpaceCreateParams> spaceValidator;

        private IMapper<Space, SpaceView> spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceCreator(ISpaceRepo spaceRepo, AbstractValidator<SpaceCreateParams> spaceValidator, IMapper<Space, SpaceView> spaceMapper) {
            this.spaceRepo = spaceRepo;
            this.spaceValidator = spaceValidator;
            this.spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        public async Task<SpaceView> Handle(SpaceCreateParams input) {
            await spaceValidator.ValidateAndThrowAsync(input);

            Space s = new Space() {
                Name = input.Name,
                Description = input.Description,
                User = input.User
            };

            await spaceRepo.Add(s);
            return spaceMapper.Map(s);
        }
        #endregion
    }
}