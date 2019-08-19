using System;

namespace Updog.Persistance {
    public sealed class UserRecord {
        #region Properties
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime JoinedDate { get; set; }
        #endregion
    }
}