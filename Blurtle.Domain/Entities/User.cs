using System;

namespace Blurtle.Domain {
    public sealed class User {
        #region Properties
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
        #endregion
    }
}
