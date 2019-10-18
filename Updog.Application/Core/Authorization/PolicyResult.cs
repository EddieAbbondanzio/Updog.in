namespace Updog.Application {
    public sealed class PolicyResult {
        #region Properties
        public bool IsAuthorized { get; }
        public string? Failure { get; }
        #endregion

        #region Constructor(s)
        public PolicyResult(bool wasSatisfied, string? failure = null) {
            IsAuthorized = wasSatisfied;
            Failure = failure;
        }
        #endregion

        #region Statics
        public static PolicyResult Authorized() => new PolicyResult(true);
        public static PolicyResult Unauthorized(string? failure = null) => new PolicyResult(false, failure);
        #endregion
    }
}