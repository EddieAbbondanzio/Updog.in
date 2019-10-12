using System;
using Updog.Domain;

namespace Updog.Domain {
    public interface ICommentFactory {
        Comment Create(CommentCreate data, User user);

        Comment Create(
            int id,
            int userId,
            int postId,
            int parentId,
            string body,
            VoteStats votes,
            DateTime creationDate,
            bool wasUpdated,
            bool wasDeleted
        );
    }
}