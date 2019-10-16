
namespace Updog.Domain {
    /// <summary>
    /// Role that gives a user some extra permissions.
    /// </summary>
    public sealed class Role : Entity<Role>, IUserEntity {
        #region Constant(s)
        public const string SiteWideDomain = "*";
        #endregion

        #region Properties
        public int UserId { get; }
        public RoleType Type { get; }
        public string Domain { get; }
        #endregion

        #region Constructor(s)
        internal Role(int id, int userId, RoleType type, string domain) {
            Id = id;
            UserId = userId;
            Type = type;
            Domain = domain;
        }

        internal Role(int userId, RoleType type, string domain) {
            UserId = userId;
            Type = type;
            Domain = domain;
        }
        #endregion
    }
}