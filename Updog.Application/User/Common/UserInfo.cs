using System;

namespace Updog.Application {
    public sealed class UserInfo {
        #region Properties
        public int Id { get; }

        public string Email { get; }

        public string Username { get; }

        public DateTime JoinedDate { get; }
        #endregion

        #region Constructor(s)
        public UserInfo(int id, string email, string username, DateTime joinedDate) {
            Id = id;
            Email = email;
            Username = username;
            JoinedDate = joinedDate;
        }
        #endregion  
    }
}