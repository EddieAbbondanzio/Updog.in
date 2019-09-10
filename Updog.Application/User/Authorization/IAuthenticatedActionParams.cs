using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interface for parameters that go to an action that needs a user associated
    /// with it to implement.
    /// </summary>
    public interface IAuthenticatedActionParams : IParams {
        #region Properties
        /// <summary>
        /// The user performing the action.
        /// </summary>
        /// <value></value>
        User? User { get; }
        #endregion
    }
}