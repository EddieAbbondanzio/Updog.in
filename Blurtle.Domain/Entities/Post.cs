using System;

namespace Blurtle.Domain {
    public sealed class Post {
        #region Properties
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        #endregion
    }
}