using System;

namespace Updog.Application {
    /// <summary>
    /// Exception for when authentication of a user fails.
    /// </summary>
    public sealed class AuthenticationException : Exception {
        #region Constructor(s)
        public AuthenticationException(string message) : base(message) { }
        #endregion
    }
}