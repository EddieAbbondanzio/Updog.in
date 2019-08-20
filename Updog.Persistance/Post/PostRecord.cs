using System;
using Updog.Domain;

namespace Updog.Persistance {
    /// <summary>
    /// A database entity for the post table.
    /// </summary>
    public sealed class PostRecord {
        #region Properties
        public int Id { get; set; }

        public int UserId { get; set; }

        public PostType Type { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        public bool WasUpdated { get; set; }

        public bool WasDeleted { get; set; }

        public int CommentCount { get; set; }
        #endregion
    }
}