using System;

namespace Blurtle.Domain {
    /// <summary>
    /// A post of the website.
    /// </summary>
    public sealed class Post : IEntity {
        #region Constants
        /// <summary>
        /// The max # of characters allowed in a post title.
        /// </summary>
        public const int TitleMaxLength = 300;

        /// <summary>
        /// The max # of characters allowed in a post body.
        /// </summary>
        public const int BodyMaxLength = 10_000;
        #endregion

        #region Properties
        /// <summary>
        /// The content type of the post.
        /// </summary>
        public PostType Type { get; set; }

        /// <summary>
        /// Unique numeric Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the post.
        /// </summary>
        /// <value></value>
        public string Title { get; set; }

        /// <summary>
        /// The body of the post. Either a URL or text body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// When the post was created.
        /// </summary>
        /// <value></value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// OP of the post.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// If the post was editted by a user.
        /// </summary>
        public bool WasEditted { get; set; }
        #endregion

        #region Statics
        /// <summary>
        /// Create a new text post.
        /// </summary>
        /// <param name="title">The title of the post.</param>
        /// <param name="body">The text body of the post.</param>
        /// <param name="userId">The user that created it.</param>
        public static Post Text(string title, string body, int userId) => new Post() {
            Type = PostType.Text,
            Title = title,
            Body = body,
            CreationDate = DateTime.UtcNow,
            UserId = userId
        };

        /// <summary>
        /// Create a new link post.
        /// </summary>
        /// <param name="title">The title of the post.</param>
        /// <param name="body">The link body of the post.</param>
        /// <param name="userId">The user that created it.</param>
        public static Post Link(string title, string body, int userId) => new Post() {
            Type = PostType.Link,
            Title = title,
            Body = body,
            CreationDate = DateTime.UtcNow,
            UserId = userId
        };
        #endregion
    }
}