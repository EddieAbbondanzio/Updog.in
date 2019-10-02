using System.Threading.Tasks;

namespace Updog.Application {
    /// <summary>
    /// Interface for read actions to implement.
    /// </summary>
    public abstract class QueryHandler<TQuery> : IActionHandler<TQuery> where TQuery : class, IQuery {
        #region Fields
        private IDatabase database;
        #endregion

        #region Constructor(s)
        public QueryHandler(IDatabase database) {
            this.database = database;
        }
        #endregion

        #region Publics
        public async Task Execute(TQuery query, IOutputPort outputPort) {
            using (var dbContext = database.GetContext()) {
                await ExecuteQuery(new ExecutionContext<TQuery>(query, dbContext, outputPort));
            }
        }
        #endregion

        #region Privates
        protected abstract Task ExecuteQuery(ExecutionContext<TQuery> context);
        #endregion
    }
}
