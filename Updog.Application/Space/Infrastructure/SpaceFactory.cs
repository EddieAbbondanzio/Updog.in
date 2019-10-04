using System;
using Updog.Domain;

namespace Updog.Application {
    public sealed class SpaceFactory : ISpaceFactory {
        public Space CreateFromCommand(SpaceCreateCommand command) => new Space() {
            Name = command.Name,
            Description = command.Description,
            User = command.User,
            CreationDate = DateTime.UtcNow
        };
    }
}