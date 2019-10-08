namespace Updog.Application {
    public sealed class InsertResult : CommandResult {
        #region Properties
        public int InsertedId { get; }
        #endregion

        #region Constructor(s)
        public InsertResult(bool success, int insertedId) : base(success) {
            InsertedId = insertedId;
        }
        #endregion
    }
}