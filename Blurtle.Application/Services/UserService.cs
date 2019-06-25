using System;
using System.Threading.Tasks;
using Blurtle.Domain;

namespace Blurtle.Application {
    /// <summary>
    /// Service for managing user's in the database.
    /// </summary>
    public sealed class UserService : IUserService {
        #region Fields
        /// <summary>
        /// CRUD interface of users in the database.
        /// </summary>
        private IUserRepo userRepo;

        /// <summary>
        /// Hashes and verifies passwords.
        /// </summary>
        private IPasswordHasher passwordHasher;
        #endregion

        #region Publics
        /// <summary>
        /// Find a user by their unique numeric id.
        /// </summary>
        /// <param name="id">The id to look for.</param>
        /// <returns>The user found (ifa any).</returns>
        public async Task<User> FindUserById(int id) => await userRepo.FindById(id);

        /// <summary>
        /// Find a username via their unique username.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <returns>The user found (if any).</returns>
        public async Task<User> FindUserByUsername(string username) => await userRepo.FindByUsername(username);

        /// <summary>
        /// Register a new user with the website.
        /// </summary>
        /// <param name="username">The username they want to use.</param>
        /// <param name="password">The password they want to use.</param>
        /// <param name="email">A contact email if one was provided.</param>
        /// <returns>The newly created user.</returns>
        public async Task<User> RegisterUser(string username, string password, string email = null) {
            bool isUsernameAvailable = await IsUsernameAvailable(username);

            if (!isUsernameAvailable) {
                throw new InvalidOperationException("Username is not available.");
            }

            //First check to see if all 3 things are legal

            throw new NotImplementedException();
        }

        /// <summary>
        /// Log in an existing user.
        /// </summary>
        /// <param name="username">The username to authenticate under.</param>
        /// <param name="password">The password to authenticate with.</param>
        /// <returns>The logged in user.</returns>
        public async Task<User> LoginUser(string username, string password) {
            User user = await userRepo.FindByUsername(username);

            if (user == null) {
                return null;
            }

            bool isPasswordValid = passwordHasher.Verify(password, user.PasswordHash);

            if (!isPasswordValid) {
                return null;
            }

            return user;
        }

        /// <summary>
        /// Check to see if a username has already been claimed, or it's banned from use.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username can be used.</returns>
        public async Task<bool> IsUsernameAvailable(string username) => (await userRepo.FindByUsername(username)) == null;
        #endregion
    }
}