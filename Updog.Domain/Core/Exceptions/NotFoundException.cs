using System;

namespace Updog.Domain {
    public sealed class NotFoundException : Exception {
        #region Constructor(s)
        public NotFoundException(string message = "The requested resource was not found.") : base(message) { }
        #endregion
    }
}