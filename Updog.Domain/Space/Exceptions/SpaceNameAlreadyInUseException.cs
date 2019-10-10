using System;

namespace Updog.Domain {
    public sealed class SpaceNameAlreadyInUseException : Exception {
        #region Constructor(s)
        public SpaceNameAlreadyInUseException(string message = "Space name is already in use.") : base(message) { }
        #endregion
    }
}