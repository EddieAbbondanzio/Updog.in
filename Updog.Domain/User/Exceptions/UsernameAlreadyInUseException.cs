using System;

namespace Updog.Domain {
    public sealed class UsernameAlreadyInUseException : Exception {
        #region Constructor(s)
        public UsernameAlreadyInUseException(string message = "Username is already in use.") : base(message) { }
        #endregion
    }
}