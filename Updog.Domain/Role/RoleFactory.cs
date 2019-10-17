namespace Updog.Domain {
    public sealed class RoleFactory : IRoleFactory {
        #region Publics
        public Role Create(int id, int userId, RoleType type, string domain) => new Role(id, userId, type, domain);
        public Role CreateAdminRole(User user) => new Role(user.Id, RoleType.Admin, Role.SiteWideDomain);
        public Role CreateModeratorRole(User user, string space) => new Role(user.Id, RoleType.Moderator, space);
        #endregion
    }
}