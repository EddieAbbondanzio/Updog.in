using System;

namespace Updog.Application {
    public sealed class UserInfo {
        #region Properties
        public string Email { get; }

        public string Username { get; }

        public DateTime JoinedDate { get; }
        #endregion

        #region Constructor(s)
        public UserInfo(string email, string username, DateTime joinedDate) {
            Email = email;
            Username = username;
            JoinedDate = joinedDate;
        }
        #endregion  
    }
}