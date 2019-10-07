using System.Threading.Tasks;

namespace Updog.Application {
    /// <summary>
    /// Interface for read actions to implement.
    /// </summary>
    public abstract class QueryHandler<TQuery> : IActionHandler<TQuery> where TQuery : class, IQuery {
        #region Publics
        public async Task Execute(TQuery query, IOutputPort outputPort) {
            await ExecuteQuery(new ExecutionContext<TQuery>(query, outputPort));
        }
        #endregion

        #region Privates
        protected abstract Task ExecuteQuery(ExecutionContext<TQuery> context);
        #endregion
    }
}
