using System;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFactory : IPostFactory {
        #region Publics
        public Post Create(PostCreationData creationData, Space space, User user) => new Post() {
            Type = creationData.Type,
            Title = creationData.Title,
            Body = creationData.Body,
            CreationDate = DateTime.UtcNow,
            Space = space,
            User = user
        };
        #endregion
    }
}