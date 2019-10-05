using System;
using System.Collections.Generic;

namespace Updog.Domain {
    /// <summary>
    /// A comment associated with a post.
    /// </summary>
    public sealed class Comment : VotableEntity, IUserEntity, ISoftDeletable {
        #region Constants
        /// <summary>
        /// The maximum number of characters in a comment body.
        /// </summary>
        public const int BodyMaxLength = 10_000;

        /// <summary>
        /// Number of comments to get per page.
        /// </summary>
        public const int PageSize = 500;
        #endregion

        #region Properties
        public override VotableEntityType EntityType => VotableEntityType.Comment;

        /// <summary>
        /// The parent post.
        /// </summary>
        public int PostId { get; set; }

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
        /// Get the child count of every root and nested child comment.
        /// </summary>
        public int ChildCount() {
            int count = Children.Count;

            foreach (Comment child in Children) {
                count += child.ChildCount();
            }

            return count;
        }


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