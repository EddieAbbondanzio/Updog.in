using System;
using System.Collections.Generic;

namespace Updog.Domain {
    /// <summary>
    /// A comment associated with a post.
    /// </summary>
    public sealed class Comment : IEntity {
        #region Constants
        /// <summary>
        /// The maximum number of characters in a comment body.
        /// </summary>
        public const int BodyMaxLength = 10_000;

        public const int PageSize = 500;
        #endregion

        #region Properties
        /// <summary>
        /// The ID of the comment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The ID of the user that made the comment.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// The parent post.
        /// </summary>
        public Post Post { get; set; } = null!;

        /// <summary>
        /// The parent comment (if null it is a top level comment).
        /// </summary>
        public Comment? Parent { get; set; }

        /// <summary>
        /// The comment children.
        /// </summary>
        public List<Comment> Children { get; set; } = new List<Comment>();

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; set; } = "";

        /// <summary>
        /// The date the comment was made.
        /// </summary>
        /// <value></value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Flag to show if the comment was updated.
        /// </summary>
        public bool WasUpdated { get; set; }

        /// <summary>
        /// Soft delete flag.
        /// </summary>
        public bool WasDeleted { get; set; }
        #endregion

        #region Publics
        /// <summary>
        /// Check to see if the comment is equal to another object.
        /// </summary>
        /// <param name="obj">The other object to check.</param>
        /// <returns>True if the other object matches the comment.</returns>
        public override bool Equals(object obj) {
            Comment? c = obj as Comment;

            if (c == null) {
                return false;
            }

            return Equals(c);
        }

        /// <summary>
        /// Check to see if two comments are equivalent.
        /// </summary>
        /// <param name="c">The other comment to check.</param>
        /// <returns>True if the comments match.</returns>
        public bool Equals(Comment c) => c.Id == this.Id;

        /// <summary>
        /// Get a unique hashcode of the object.
        /// </summary>
        /// <returns>The unique hashcode.</returns>
        public override int GetHashCode() => Id;
        #endregion

    }
}