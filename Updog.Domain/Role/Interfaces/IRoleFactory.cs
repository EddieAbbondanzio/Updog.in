namespace Updog.Domain {
    public interface IRoleFactory {
        Role Create(int id, int userId, RoleType type, string domain);
        Role CreateAdminRole(User user);
        Role CreateModeratorRole(User user, string space);
    }
}