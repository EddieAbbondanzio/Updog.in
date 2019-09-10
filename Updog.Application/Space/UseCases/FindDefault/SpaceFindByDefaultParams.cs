using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Parameters to find spaces by default status.
    /// </summary>
    public sealed class SpaceFindByDefaultParams : IAnonymousActionParams {
        #region Properties
        /// <summary>
        /// The user performing the look up.
        /// </summary>
        public User? User { get; }
        #endregion

        #region Constructor(s)
        public SpaceFindByDefaultParams(User? user = null) {
            User = user;
        }
        #endregion
    }
}