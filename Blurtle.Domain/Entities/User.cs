using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Blurtle.Domain {
    public sealed class User : ClaimsPrincipal, IEntity {
        #region Properties
        public int Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime JoinedDate { get; set; }
        #endregion
    }
}
