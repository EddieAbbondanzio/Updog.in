using System;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceFactory : ISpaceFactory {
        public Space Create(SpaceCreationData creationData, User user) => new Space() {
            Name = creationData.Name,
            Description = creationData.Description,
            User = user,
            CreationDate = DateTime.UtcNow
        };
    }
}