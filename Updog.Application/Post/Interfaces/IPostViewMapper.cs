using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a post entity to it's view.
    /// </summary>
    public interface IPostViewMapper : IMapper<Post, PostView> { }
}