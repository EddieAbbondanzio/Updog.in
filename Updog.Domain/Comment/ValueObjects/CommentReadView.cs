using System;
using System.Collections.Generic;

namespace Updog.Domain {
    /// <summary>
    /// Value object to return from the CommentReader for viewing comments.
    /// </summary>
    public sealed class CommentReadView : IValueObject {
        #region Properties
        public int Id { get; set; }
        public int PostId { get; set; }
        public UserReadView User { get; set; } = null!;
        public VoteReadView? Vote { get; set; } = null!;
        public CommentReadView? Parent { get; set; }
        public List<CommentReadView> Children { get; set; } = new List<CommentReadView>();
        public string Body { get; set; } = "";
        public DateTime CreationDate { get; set; }
        public bool WasUpdated { get; set; }
        public bool WasDeleted { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        #endregion
    }
}