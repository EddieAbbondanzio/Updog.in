using System;
using Updog.Domain;

namespace Updog.Domain {
    public sealed class SpaceFactory : ISpaceFactory {
        public Space Create(SpaceCreate createData, User user) => new Space(createData, user);
    }
}