using System.Data.Common;

namespace Updog.Domain {
    public abstract class DatabaseReader : DatabaseInteractor {
        #region Constructor(s)
        public DatabaseReader(IDatabase database) : base(database) { }
        #endregion
    }

    /// <summary>
    /// Read only access to the database. Useful for extracting value objects.
    /// </summary>
    public abstract class DatabaseReader<TValueObject> : DatabaseReader where TValueObject : class, IValueObject {
        #region Constructor(s)
        public DatabaseReader(IDatabase database) : base(database) { }
        #endregion

        #region Privates
        protected TReader GetReader<TReader>() where TReader : class, IReader => Context.GetReader<TReader>();
        #endregion
    }
}