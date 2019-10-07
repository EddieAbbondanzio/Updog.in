using System;

namespace Updog.Domain {
    public sealed class PostReadView : IValueObject {
        #region Properties
        public int Id { get; set; }
        public UserReadView User { get; set; } = null!;
        public SpaceReadView Space { get; set; } = null!;
        public PostType Type { get; set; }
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public DateTime CreationDate { get; set; }
        public bool WasUpdated { get; set; }
        public bool WasDeleted { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        #endregion
    }
}