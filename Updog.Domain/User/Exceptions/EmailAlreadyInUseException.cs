using System;

namespace Updog.Domain {
    public sealed class EmailAlreadyInUseException : Exception {
        #region Constructor(s)
        public EmailAlreadyInUseException(string message = "Email is already in use.") : base(message) { }
        #endregion
    }
}