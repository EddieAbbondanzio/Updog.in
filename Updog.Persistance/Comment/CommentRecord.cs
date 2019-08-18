using System;

namespace Updog.Persistance {
    internal sealed class CommentRecord {
        #region Properties
        public int Id { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public int ParentId { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        public bool WasUpdated { get; set; }

        public bool WasDeleted { get; set; }
        #endregion
    }
}