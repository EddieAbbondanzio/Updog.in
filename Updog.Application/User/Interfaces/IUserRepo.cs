using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// CRUD interface for managing user's in the database.
    /// </summary>
    public interface IUserRepo : IRepo<User> {
        #region Publics
        /// <summary>
        /// Find a user by their username.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <returns>The user with the username.</returns>
        Task<User> FindByUsername(string username);

        /// <summary>
        /// Find a user via their contact email.
        /// </summary>
        /// <param name="email">The email to look for.</param>
        /// <returns>The user found (if any).</returns>
        Task<User> FindByEmail(string email);
        #endregion
    }
}