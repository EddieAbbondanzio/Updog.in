using System;
using Updog.Domain;

namespace Updog.Domain {
    public interface IPostFactory : IFactory<Post> {
        Post Create(PostCreate creationData, Space space, User user);
        Post Create(int id, int userId, int spaceId, PostType type, string title, string body, DateTime creationDate, int commentCount, int upvotes, int downvotes, bool wasUpdated, bool wasDeleted);
    }
}