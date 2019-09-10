using System;

namespace Updog.Persistance {
    /// <summary>
    /// Data transfer object for a comment entity.
    /// </summary>
    public sealed class CommentRecord {
        #region Properties
        /// <summary>
        /// The primary key of the comment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key of the user that it belongs to.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Foreign key of the post it is under.
        /// </summary>
        /// <value></value>
        public int PostId { get; set; }

        /// <summary>
        /// Foreign key of the comment that it is a child (nested) of.
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; set; } = "";

        /// <summary>
        /// THe date/time the comment was made.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// If the comment has been modified since it was created.
        /// </summary>
        public bool WasUpdated { get; set; }

        /// <summary>
        /// If the post was deleted.
        /// </summary>
        public bool WasDeleted { get; set; }

        /// <summary>
        /// The number of upvotes it's recieved.
        /// </summary>
        public int Upvotes { get; set; }

        /// <summary>
        /// The number of downvotes it's recieved.
        /// </summary>
        public int Downvotes { get; set; }
        #endregion
    }
}