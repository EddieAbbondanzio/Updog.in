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

        /// <summary>
        /// The default page size of a page of posts.
        /// </summary>
        public const int PageSize = 25;
        #endregion

        #region Properties
        /// <summary>
        /// Unique numeric Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// OP of the post.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// The sub space it was submitted to.
        /// </summary>
        public Space Space { get; set; } = null!;

        /// <summary>
        /// The content type of the post.
        /// </summary>
        public PostType Type { get; set; }

        /// <summary>
        /// The title of the post.
        /// </summary>
        /// <value></value>
        public string Title { get; set; } = "";

        /// <summary>
        /// The body of the post. Either a URL or text body.
        /// </summary>
        public string Body { get; set; } = "";

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
        /// The number of comments on the post.
        /// </summary>
        /// <value></value>
        public int CommentCount { get; set; }
        #endregion

        #region Publics
        /// <summary>
        /// Check t osee if the post matches another object.
        /// </summary>
        /// <param name="obj">The other object to check.</param>
        /// <returns>True if the post matches</returns>
        public override bool Equals(object obj) {
            Post? p = obj as Post;

            if (p == null) {
                return false;
            }

            return Equals(p);
        }

        /// <summary>
        /// Check to see if the post matches another post.
        /// </summary>
        /// <param name="post">The other post to check.</param>
        /// <returns>True if the post match.</returns>
        public bool Equals(Post post) => Id == post.Id;

        /// <summary>
        /// Get a unique hashcode of the object.
        /// </summary>
        /// <returns>The unique hashcode.</returns>
        public override int GetHashCode() => Id;
        #endregion
    }
}