using System;

namespace Blurtle.Domain {
    /// <summary>
    /// A post of the website.
    /// </summary>
    public sealed class Post : IEntity {
        #region Properties
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
        /// The content type of the post.
        /// </summary>
        public PostType Type { get; set; }

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
    }
}