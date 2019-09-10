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
        /// If the comment has been modified.
        /// </summary>
        public bool WasUpdated { get; }

        /// <summary>
        /// If the comment has been deleted.
        /// </summary>
        public bool WasDeleted { get; }

        /// <summary>
        /// How many upvotes the comment has recieved.
        /// </summary>
        public int Upvotes { get; }

        /// <summary>
        /// How many downvotes the comment has recieved.
        /// </summary>
        public int Downvotes { get; }

        /// <summary>
        /// The vote of the user viewing it.
        /// </summary>
        /// <value></value>
        public VoteView Vote { get; }

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
        /// <param name="wasUpdated">If the comment was modified</param>
        /// <param name="wasDeleted">If the comment was deleted</param>
        /// <param name="upvotes">The number of upvotes.</param>
        /// <param name="downvotes">The number of downvotes.</param>
        /// <param name="vote">The vote of the user viewing it</param>
        public CommentView(int id, UserView user, string body, DateTime creationDate, bool wasUpdated, bool wasDeleted, int upvotes, int downvotes, VoteView vote) {
            Id = id;
            User = user;
            Body = body;
            CreationDate = creationDate;
            WasUpdated = wasUpdated;
            WasDeleted = wasDeleted;
            Upvotes = upvotes;
            Downvotes = downvotes;
            Vote = vote;
            Children = new List<CommentView>();
        }

        /// <summary>
        /// Create a new comment view.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        /// <param name="user">The user that made the comment.</param>
        /// <param name="body">The text of the comment.</param>
        /// <param name="creationDate">The time of commenting</param>
        /// <param name="wasUpdated">If the comment was modified</param>
        /// <param name="wasDeleted">If the comment was deleted</param>
        /// <param name="upvotes">The number of upvotes.</param>
        /// <param name="downvotes">The number of downvotes.</param>
        /// <param name="vote">The vote of the user viewing it</param>
        /// <param name="children">The children comments</param>
        public CommentView(int id, UserView user, string body, DateTime creationDate, bool wasUpdated, bool wasDeleted, int upvotes, int downvotes, VoteView vote, List<CommentView> children) {
            Id = id;
            User = user;
            Body = body;
            CreationDate = creationDate;
            WasUpdated = wasUpdated;
            WasDeleted = wasDeleted;
            Upvotes = upvotes;
            Downvotes = downvotes;
            Vote = vote;
            Children = children;
        }
        #endregion
    }
}