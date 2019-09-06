using System;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// Data transfer object for a post.
    /// </summary>
    public sealed class PostRecord {
        #region Properties
        /// <summary>
        /// Primary key of the post record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key of the user that it belongs to.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Foreign key of the space it was posted to.
        /// </summary>
        public int SpaceId { get; set; }

        /// <summary>
        /// Type flag.
        /// </summary>
        public PostType Type { get; set; }

        /// <summary>
        /// Title of the post.
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Body (or URL) of the post.
        /// </summary>
        public string Body { get; set; } = "";

        /// <summary>
        /// When the post was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Has the post been updated?
        /// </summary>
        public bool WasUpdated { get; set; }

        /// <summary>
        /// Has the post been deleted?
        /// </summary>
        public bool WasDeleted { get; set; }

        /// <summary>
        /// How many comments have been made on the post.
        /// </summary>
        public int CommentCount { get; set; }
        #endregion
    }
}