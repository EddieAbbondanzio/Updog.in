using System.Threading.Tasks;

namespace Updog.Application {
    /// <summary>
    /// Interface for commands and queries to implement.
    /// </summary>
    public interface IActionHandler<in TAction, TOutput> where TAction : class {
        #region Publics
        /// <summary>
        /// Execute the action.
        /// </summary>
        /// <param name="action">The action parameters</param>
        Task<TOutput> Execute(TAction action);
        #endregion
    }
}