using System;
using Updog.Domain;

namespace Updog.Application {
    public sealed class CommentFactory : ICommentFactory {
        #region Publics
        public Comment Create(CommentCreateData data, User user) => new Comment(data, user);
        #endregion
    }
}