using System;
using Updog.Domain;

namespace Updog.Domain {
    public sealed class PostFactory : IPostFactory {
        #region Publics
        public Post Create(PostCreate creationData, Space space, User user) => new Post(creationData, space, user);

        public Post Create(int id, int userId, int spaceId, PostType type, string title, string body, DateTime creationDate, int commentCount, int upvotes, int downvotes, bool wasUpdated, bool wasDeleted) =>
        new Post(id, userId, spaceId, type, title, body, creationDate, commentCount, new VoteStats(upvotes, downvotes), wasUpdated, wasDeleted);
        #endregion
    }
}