using System;
using Updog.Domain;

namespace Updog.Domain {
    public sealed class SpaceFactory : ISpaceFactory {
        public Space Create(SpaceCreate createData, User user) => new Space(createData, user);
        public Space Create(int id, int userId, string name, string description, DateTime creationDate, int subscriberCount, bool isDefault) => new Space(id, userId, name, description, creationDate, subscriberCount, isDefault);
    }
}