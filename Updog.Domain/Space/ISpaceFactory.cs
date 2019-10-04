
namespace Updog.Domain {
    public interface ISpaceFactory : IFactory<Space> {
        Space Create(SpaceCreationData creationData, User user);
    }
}