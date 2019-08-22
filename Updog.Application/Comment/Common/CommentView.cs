using System;
using System.Collections.Generic;

namespace Updog.Application {
    public sealed class CommentView : IView {
        #region Properties
        /// <summary>
        /// The unique ID of the comment.
        /// </summary>
        /// <value></value>
        public int Id { get; }

        /// <summary>
        /// The user that created the comment.
        /// </summary>
        public UserView User { get; }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// When the comment was made.
        /// </summary>
        public DateTime CreationDate { get; }

        /// <summary>
        /// The nested comments of this comment.
        /// </summary>
        public List<CommentView> Children { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment view.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        /// <param name="user">The user that made the comment.</param>
        /// <param name="body">The text of the comment.</param>
        /// <param name="creationDate">The time of commenting</param>
        /// <param name="children">The children comments</param>
        public CommentView(int id, UserView user, string body, DateTime creationDate, List<CommentView> children = null) {
            Id = id;
            User = user;
            Body = body;
            CreationDate = creationDate;
            Children = children ?? new List<CommentView>();
        }
        #endregion
    }
}