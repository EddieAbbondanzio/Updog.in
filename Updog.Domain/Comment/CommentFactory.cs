using System;
using Updog.Domain;

namespace Updog.Domain {
    public sealed class CommentFactory : ICommentFactory {
        #region Publics
        public Comment Create(CommentCreate data, User user) => new Comment(data, user);
        #endregion
    }
}