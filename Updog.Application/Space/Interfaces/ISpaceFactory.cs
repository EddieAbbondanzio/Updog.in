using Updog.Domain;

namespace Updog.Application {
    public interface ISpaceFactory : IFactory<Space> {
        Space CreateFromCommand(SpaceCreateCommand command);
    }
}