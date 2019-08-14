using System;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Information about a post.
    /// </summary>
    public sealed class PostInfo {
        #region Properties
        /// <summary>
        /// The unqiue ID of the post.
        /// </summary>
        /// <value></value>
        public int Id { get; }

        /// <summary>
        /// Type flag.
        /// </summary>
        public PostType Type { get; }

        /// <summary>
        /// The title of the post.
        /// </summary>
        /// <value></value>
        public string Title { get; }

        /// <summary>
        /// The body of the post. Either a URL or text.
        /// </summary>
        /// <value></value>
        public string Body { get; }

        /// <summary>
        /// The name of the user that created it.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// When the post was made.
        /// </summary>
        public DateTime Date { get; }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post info.
        /// </summary>
        /// <param name="id">The unique ID of the post.</param>
        /// <param name="type">The type of post it is.</param>
        /// <param name="title">The title of the post.</param>
        /// <param name="body">The body of the post.</param>
        /// <param name="author">The author of the post.</param>
        /// <param name="date">The date of posting</param>
        public PostInfo(int id, PostType type, string title, string body, string author, DateTime date) {
            Id = id;
            Type = type;
            Title = title;
            Body = body;
            Author = author;
            Date = date;
        }
        #endregion
    }
}