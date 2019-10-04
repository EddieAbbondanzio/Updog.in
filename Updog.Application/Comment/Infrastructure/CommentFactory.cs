using System;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFactory : ICommentFactory {
        #region Publics
        public Comment Create(CommentCreationData data, User user) => new Comment() {
            PostId = data.PostId,
            Body = data.Body,
            CreationDate = DateTime.UtcNow,
            User = user
        };
        #endregion
    }
}