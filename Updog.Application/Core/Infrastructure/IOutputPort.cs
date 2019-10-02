
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Interface for the presenter layer to implement.
    /// </summary>
    public interface IOutputPort {
        #region Publics
        /// <summary>
        /// Successful output. 
        /// </summary>
        void Success<TResult>(TResult? result = null) where TResult : class;

        /// <summary>
        /// Value being searched for was not found.
        /// </summary>
        void NotFound<TResult>(TResult? result = null) where TResult : class;

        /// <summary>
        /// Input was bad, or invalid.
        /// </summary>
        void BadInput<TResult>(TResult? result = null) where TResult : class;

        /// <summary>
        /// Unauthorized action was performed.
        /// </summary>
        void Unauthorized<TResult>(TResult? result = null) where TResult : class;
        #endregion
    }
}