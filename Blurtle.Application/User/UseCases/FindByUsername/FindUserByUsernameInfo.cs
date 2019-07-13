using System;

namespace Blurtle.Application {
    public sealed class FindUserByUsernameInfo {
        #region Properties
        public string Username { get; }

        public DateTime JoinedDate { get; }
        #endregion

        #region Constructor(s)
        public FindUserByUsernameInfo(string username, DateTime joinedDate) {
            Username = username;
            JoinedDate = joinedDate;
        }
        #endregion  
    }
}