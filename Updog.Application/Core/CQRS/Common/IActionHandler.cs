using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interface for commands and queries to implement.
    /// </summary>
    public interface IActionHandler<in TAction, TOutput> where TAction : class {
        #region Publics
        /*
        * Init method / service provider are code smells, but I don't want to do constructor based injection
        * for every single command / query handler. I'm lazy and tired of boilerplate.
        */

        /// <summary>
        /// Initalize the handler for use.
        /// </summary>
        /// <param name="provider">The service provider.</param>
        void Init(IServiceProvider provider);

        /// <summary>
        /// Execute the action.
        /// </summary>
        /// <param name="action">The action parameters</param>
        Task<Either<TOutput, Error>> Execute(TAction action);
        #endregion
    }
}