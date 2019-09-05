using System.Threading.Tasks;
using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to create a new space.
    /// </summary>
    public sealed class SpaceCreator : IInteractor<SpaceCreateParams, SpaceView> {
        #region Fields
        private ISpaceRepo _spaceRepo;

        private AbstractValidator<SpaceCreateParams> _spaceValidator;

        private ISpaceViewMapper _spaceMapper;
        #endregion

        #region Constructor(s)
        public SpaceCreator(ISpaceRepo spaceRepo, AbstractValidator<SpaceCreateParams> spaceValidator, ISpaceViewMapper spaceMapper) {
            _spaceRepo = spaceRepo;
            _spaceValidator = spaceValidator;
            _spaceMapper = spaceMapper;
        }
        #endregion

        #region Publics
        public async Task<SpaceView> Handle(SpaceCreateParams input) {
            await _spaceValidator.ValidateAndThrowAsync(input);

            Space s = new Space() {
                Name = input.Name,
                Description = input.Description,
                User = input.User
            };

            await _spaceRepo.Add(s);
            return _spaceMapper.Map(s);
        }
        #endregion
    }
}