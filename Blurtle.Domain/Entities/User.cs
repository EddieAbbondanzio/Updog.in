using System;

namespace Blurtle.Domain {
    public sealed class User : Entity {
        #region Properties

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime JoinedDate { get; set; }
        #endregion
    }
}
