using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Updog.Domain {
    /// <summary>
    /// A user of the site.
    /// </summary>
    public partial class User : ClaimsPrincipal, IEntity {
        #region Constants
        /// <summary>
        /// Minimum number of characters in a password.
        /// </summary>
        public const int PasswordMinLength = 8;
        public const int UsernameMinLength = 4;
        public const int UsernameMaxLength = 24;
        public const int EmailMaxLength = 64;
        #endregion

        #region Properties
        /// <summary>
        /// The unique ID of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The unique display name of the user.
        /// </summary>
        public string Username { get; set; } = "";

        /// <summary>
        /// Contact email (if any)
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Super secret hash of the password.
        /// </summary>
        public string PasswordHash { get; set; } = "";

        /// <summary>
        /// Date the user registered.
        /// </summary>
        public DateTime JoinedDate { get; set; }

        /// <summary>
        /// The karma count for posts.
        /// </summary>
        public int PostKarma { get; set; }

        /// <summary>
        /// The karma count for comments.
        /// </summary>
        public int CommentKarma { get; set; }
        #endregion

        #region Publics
        /// <summary>
        /// Check to see if another object matches the current user.
        /// </summary>
        /// <param name="obj">The other object to check.</param>
        /// <returns>True if the user matches the object.</returns>
        public override bool Equals(object obj) {
            User? u = obj as User;

            if (u == null) {
                return false;
            }

            return Equals(u);
        }

        /// <summary>
        /// Check to see if the user matches another user.
        /// </summary>
        /// <param name="user">The other user to check.</param>
        /// <returns>True if the user match.</returns>
        public bool Equals(User user) => Id == user.Id;

        /// <summary>
        /// Get a unique hashcode of the object.
        /// </summary>
        /// <returns>The unique hashcode.</returns>
        public override int GetHashCode() => Id;
        #endregion
    }
}
