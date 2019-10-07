using System.Data.Common;

namespace Updog.Domain {
    public abstract class DatabaseReader { }

    /// <summary>
    /// Read only access to the database. Useful for extracting value objects.
    /// </summary>
    public abstract class DatabaseReader<TValueObject> : DatabaseReader where TValueObject : class, IValueObject {
        #region Properties
        protected DbConnection Connection => context.Connection;
        #endregion

        #region Fields
        private DatabaseContext context;
        #endregion

        #region Constructor(s)
        public DatabaseReader(IDatabase database) {
            this.context = database.GetContext();
        }
        #endregion

        #region Privates
        protected TReader GetReader<TReader>() where TReader : class, IReader => context.GetReader<TReader>();
        #endregion
    }
}