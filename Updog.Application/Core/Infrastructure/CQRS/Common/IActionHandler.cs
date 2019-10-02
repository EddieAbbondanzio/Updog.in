using System.Threading.Tasks;

namespace Updog.Application {
    /// <summary>
    /// Interface for commands and queries to implement.
    /// </summary>
    public interface IActionHandler<in TAction> where TAction : class {
        #region Publics
        /// <summary>
        /// Execute the action.
        /// </summary>
        /// <param name="action">The action parameters</param>
        /// <param name="outputPort">Output port.</param>
        Task Execute(TAction action, IOutputPort outputPort);
        #endregion
    }
}