using System;

namespace Updog.Persistance {
    /// <summary>
    /// User data transfer object.
    /// </summary>
    internal sealed class UserRecord {
        #region Properties
        /// <summary>
        /// Primary key of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique username.
        /// </summary>
        public string Username { get; set; } = "";

        /// <summary>
        /// Contact email (if any).
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Super secret hash.
        /// </summary>
        public string PasswordHash { get; set; } = "";

        /// <summary>
        /// The date / time of when they created their account.
        /// </summary>
        public DateTime JoinedDate { get; set; }

        /// <summary>
        /// The post karma the user has acrued.
        /// </summary>
        public int PostKarma { get; set; }

        /// <summary>
        /// The comment karma the user has acrued.
        /// </summary>
        public int CommentKarma { get; set; }
        #endregion
    }
}