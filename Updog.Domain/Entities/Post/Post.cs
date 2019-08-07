using System;

namespace Updog.Domain {
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
        /// Unique numeric Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// OP of the post.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The content type of the post.
        /// </summary>
        public PostType Type { get; set; }

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
        /// If the post was editted by a user.
        /// </summary>
        public bool WasUpdated { get; set; }

        /// <summary>
        /// If the post was deleted by a user.
        /// </summary>
        /// <value></value>
        public bool WasDeleted { get; set; }
        #endregion

        #region Constructor(s)
        public Post() { }

        /// <summary>
        /// Create a new post.
        /// </summary>
        /// <param name="type">The type of post it is.</param>
        /// <param name="title">The title of the post.</param>
        /// <param name="body">The text body of the post.</param>
        /// <param name="userId">The user that created it.</param>
        public Post(PostType type, string title, string body, int userId) {
            Type = type;
            Title = title;
            Body = body;
            CreationDate = DateTime.UtcNow;
            UserId = userId;
        }
        #endregion
    }
}