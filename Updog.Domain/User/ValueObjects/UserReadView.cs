using System;

namespace Updog.Domain {
    public sealed class UserReadView : IValueObject {
        #region Properties
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public DateTime JoinedDate { get; set; }
        public int PostKarma { get; set; }
        public int CommentKarma { get; set; }
        #endregion
    }
}