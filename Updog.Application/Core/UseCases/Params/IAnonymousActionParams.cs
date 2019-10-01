using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interface for parameters that may have a user associated with them implement.
    /// </summary>
    public interface IAnonymousActionParams : IParams {
        #region Properties
        /// <summary>
        /// The user that may or may not be associated with the action.
        /// </summary>
        User? User { get; }
        #endregion
    }
}