using System.Threading.Tasks;

namespace Updog.Domain {
    /// <summary>
    /// Service to manage roles for users.
    /// </summary>
    public interface IRoleService : IService<Role> {
        /// <summary>
        /// Add a new admin to the site.
        /// </summary>
        /// <param name="username">The username of the new admin.</param>
        /// <param name="user">The user performing this action.</param>
        Task AddAdmin(string username, User user);

        /// <summary>
        /// Remove an existing admin's powers from a user.
        /// </summary>
        /// <param name="username">The username of the user to remove admin powers.</param>
        /// <param name="user">The user performing this action.</param>
        Task RemoveAdmin(string username, User user);

        /// <summary>
        /// Add a new moderator to a space.
        /// </summary>
        /// <param name="username">The username of the new moderator.</param>
        /// <param name="space">The space they will moderate.</param>
        /// <param name="user">The user who made them a mod.</param>
        Task AddModeratorToSpace(string username, string space, User user);

        /// <summary>
        /// Remove an existing moderator from a space.
        /// </summary>
        /// <param name="username">The username of the user to take modship away from.</param>
        /// <param name="space">The space they moderate.</param>
        /// <param name="user">The user who revoked the mods powers.</param>
        Task RemoveModeratorFromSpace(string username, string space, User user);
    }
}