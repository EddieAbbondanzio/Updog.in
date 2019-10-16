namespace Updog.Domain {
    public sealed class RoleReadView : ValueObject<RoleReadView> {
        #region Properties
        public RoleType Type { get; }
        public string Domain { get; }
        #endregion

        #region Constructor(s)
        public RoleReadView(RoleType type, string domain) {
            Type = type;
            Domain = domain;
        }
        #endregion
    }
}