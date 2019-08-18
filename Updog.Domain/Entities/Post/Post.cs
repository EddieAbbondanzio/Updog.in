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
        public User User { get; set; }

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

        /// <summary>
        /// The comments of the post.
        /// </summary>
        public Comment[] Comments { get; set; }
        #endregion
    }
}