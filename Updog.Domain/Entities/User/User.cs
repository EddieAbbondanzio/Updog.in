using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Updog.Domain {
    /// <summary>
    /// A user of the site.
    /// </summary>
    public sealed class User : ClaimsPrincipal, IEntity {
        #region Properties
        /// <summary>
        /// The unique ID of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The unique display name of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Contact email (if any)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Super secret hash of the password.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Date the user registered.
        /// </summary>
        public DateTime JoinedDate { get; set; }
        #endregion
    }
}
