
using Updog.Application.Validation;

namespace Updog.Application {
    /// <summary>
    /// Interface for the presenter layer to implement.
    /// </summary>
    public interface IOutputPort {
        #region Publics
        void Success<TResult>(TResult? result = null) where TResult : class;
        void NotFound<TResult>(TResult? result = null) where TResult : class;
        void BadInput<TResult>(TResult? result = null) where TResult : class;
        void Unauthorized<TResult>(TResult? result = null) where TResult : class;
        #endregion
    }
}