using System;
using System.Threading.Tasks;
using Blurtle.Domain;
using Blurtle.Application;
using MySql.Data.MySqlClient;

namespace Blurtle.Persistance {
    /// <summary>
    /// CRUD interface for managing user's in the database.
    /// </summary>
    public sealed class UserRepo : IUserRepo {
        #region Publics
        /// <summary>
        /// Find a user by their id.
        /// </summary>
        /// <param name="id">The id to look for.</param>
        /// <returns>The user with the id.</returns>
        public async Task<User> FindById(int id) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find a user by their username.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <returns>The user with the username.</returns>
        public async Task<User> FindByUsername(string username) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a new user to the database.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public async Task Add(User user) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a user in the database.
        /// </summary>
        /// <param name="user">The user to update.</param>
        public async Task Update(User user) {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Delete a user from the database.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        public async Task Delete(User user) {
            throw new NotImplementedException();
        }
        #endregion
    }
}