namespace Updog.Application {
    public sealed class DataResult<TData> : CommandResult where TData : class {
        #region Publics
        public TData Data { get; }
        #endregion

        #region Constructor(s)
        public DataResult(bool success, TData data) : base(success) {
            Data = data;
        }
        #endregion
    }
}