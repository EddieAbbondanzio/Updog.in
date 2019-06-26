
using System.Threading.Tasks;
using Blurtle.Domain;

namespace Blurtle.Application {
    /// <summary>
    /// Service for managing users of the website.
    /// </summary>
    public interface IUserService {
        #region Publics
        /// <summary>
        /// Find a user by their unique numeric id.
        /// </summary>
        /// <param name="id">The id to look for.</param>
        /// <returns>The user found (ifa any).</returns>
        Task<User> FindUserById(int id);

        /// <summary>
        /// Find a username via their unique username.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <returns>The user found (if any).</returns>
        Task<User> FindUserByUsername(string username);

        /// <summary>
        /// Register a new user with the website.
        /// </summary>
        /// <param name="userReg">Registration info of the user.</param>
        /// <returns>The newly created user.</returns>
        Task<User> RegisterUser(UserRegistration userReg);

        /// <summary>
        /// Log in an existing user.
        /// </summary>
        /// <param name="username">The username to authenticate under.</param>
        /// <param name="password">The password to authenticate with.</param>
        /// <returns>The logged in user.</returns>
        Task<User> LoginUser(string username, string password);

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        Task UpdateUser(User user);

        /// <summary>
        /// Check to see if a username has already been claimed, or it's banned from use.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username can be used.</returns>
        Task<bool> IsUsernameAvailable(string username);
        #endregion
    }
}