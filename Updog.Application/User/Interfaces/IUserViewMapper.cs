using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Mapper to convert a user entity to it's view.
    /// </summary>
    public interface IUserViewMapper : IMapper<User, UserView> { }
}