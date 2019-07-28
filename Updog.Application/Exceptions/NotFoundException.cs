using System;

namespace Blurtle.Application {
    /// <summary>
    /// Exception for when a resource or item was not found..
    /// </summary>
    public sealed class NotFoundException : Exception {
        #region Constructor(s)
        public NotFoundException(string message = null) : base(message) { }
        #endregion
    }
}