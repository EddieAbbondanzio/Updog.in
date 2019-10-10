using System;
using Updog.Domain;

namespace Updog.Domain {
    public sealed class PostFactory : IPostFactory {
        #region Publics
        public Post Create(PostCreate creationData, Space space, User user) => new Post(creationData, space, user);
        #endregion
    }
}