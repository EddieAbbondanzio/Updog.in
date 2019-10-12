using System;
using Updog.Domain;

namespace Updog.Domain {
    public sealed class CommentFactory : ICommentFactory {
        #region Publics
        public Comment Create(CommentCreate data, User user) => new Comment(data, user);

        public Comment Create(
            int id,
            int userId,
            int postId,
            int parentId,
            string body,
            VoteStats votes,
            DateTime creationDate,
            bool wasUpdated,
            bool wasDeleted
        ) => new Comment(id, userId, postId, parentId, body, votes, creationDate, wasUpdated, wasDeleted);
        #endregion
    }
}