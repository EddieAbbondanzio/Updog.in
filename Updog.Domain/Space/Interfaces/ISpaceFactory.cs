
namespace Updog.Domain {
    public interface ISpaceFactory : IFactory<Space> {
        Space Create(SpaceCreateData creationData, User user);
    }
}