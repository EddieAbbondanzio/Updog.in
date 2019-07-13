using System;

namespace Blurtle.Application {
    public sealed class UserInfo {
        #region Properties
        public string Username { get; }

        public DateTime JoinedDate { get; }
        #endregion

        #region Constructor(s)
        public UserInfo(string username, DateTime joinedDate) {
            Username = username;
            JoinedDate = joinedDate;
        }
        #endregion  
    }
}