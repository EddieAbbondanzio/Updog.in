using System;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// A post DTO.
    /// </summary>
    public sealed class PostView : IView {
        #region Properties
        /// <summary>
        /// The unique ID of the post.
        /// </summary>
        /// <value></value>
        public int Id { get; }

        /// <summary>
        /// The type flag.
        /// </summary>
        public PostType Type { get; }

        /// <summary>
        /// The title of the post.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The text of the post.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The user that posted it.
        /// </summary>
        public UserView User { get; }

        /// <summary>
        /// The date it was created.
        /// </summary>
        public DateTime CreationDate { get; }

        /// <summary>
        /// The comments of the post.
        /// </summary>
        /// <value></value>
        public CommentView[] Comments { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post DTO.
        /// </summary>
        /// <param name="id">The ID of the post.</param>
        /// <param name="type">The type flag of the post.</param>
        /// <param name="title">The title of the post.</param>
        /// <param name="body">The body of the post.</param>
        /// <param name="user">The OP.</param>
        /// <param name="creationDate">The date the post was created on.</param>
        /// <param name="comments">The comments of the post</param>
        public PostView(int id, PostType type, string title, string body, UserView user, DateTime creationDate, CommentView[] comments) {
            Id = id;
            Type = type;
            Title = title;
            Body = body;
            User = user;
            CreationDate = creationDate;
            Comments = comments;
        }
        #endregion
    }
}