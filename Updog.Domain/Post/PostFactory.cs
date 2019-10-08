using System;
using Updog.Domain;

namespace Updog.Domain {
    public sealed class PostFactory : IPostFactory {
        #region Publics
        public Post Create(PostCreateData creationData, User user) => new Post(creationData, user);
        #endregion
    }
}