
using System;

namespace Updog.Domain {
    public interface ISpaceFactory : IFactory<Space> {
        Space Create(SpaceCreate create, User user);
        Space Create(int id, int userId, string name, string description, DateTime creationDate, int subscriberCount, bool isDefault);
    }
}