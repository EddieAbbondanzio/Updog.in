using System.Threading.Tasks;

namespace Updog.Application {
    /// <summary>
    /// Interface for read actions to implement.
    /// </summary>
    public abstract class QueryHandler<TQuery, TOutput> : IActionHandler<TQuery, TOutput> where TQuery : class, IQuery {
        #region Publics
        public async Task<TOutput> Execute(TQuery query) => await ExecuteQuery(query);
        #endregion

        #region Privates
        protected abstract Task<TOutput> ExecuteQuery(TQuery query);
        #endregion
    }
}
