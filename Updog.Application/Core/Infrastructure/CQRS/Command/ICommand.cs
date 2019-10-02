using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interface for commands to implement.
    /// </summary>
    public interface ICommand {
        #region Properties
        /// <summary>
        /// User performing the action.
        /// </summary>
        User User { get; }
        #endregion
    }
}