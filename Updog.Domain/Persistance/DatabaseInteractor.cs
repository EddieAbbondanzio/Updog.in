using System.Data.Common;

namespace Updog.Domain {
    public abstract class DatabaseInteractor {
        #region Properties
        protected DbConnection Connection => context.Connection;

        protected DatabaseContext Context => context;
        #endregion

        #region Fields
        private DatabaseContext context;
        #endregion

        #region Constructor(s)
        public DatabaseInteractor(IDatabase database) {
            this.context = database.GetContext();
        }
        #endregion
    }
}