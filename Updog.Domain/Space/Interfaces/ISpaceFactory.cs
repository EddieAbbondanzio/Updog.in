
namespace Updog.Domain {
    public interface ISpaceFactory : IFactory<Space> {
        Space Create(SpaceCreate create, User user);
    }
}