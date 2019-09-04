using System;

namespace Updog.Application {
    /// <summary>
    /// Exception for when a resource or item collides with another existing one.
    /// </summary>
    public sealed class CollisionException : Exception {
        #region Constructor(s)
        public CollisionException(string message = null) : base(message) { }
        #endregion
    }
}