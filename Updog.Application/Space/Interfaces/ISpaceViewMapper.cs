using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a space entity to it's view.
    /// </summary>
    public interface ISpaceViewMapper : IMapper<Space, SpaceView> { }
}