using System;
using System.Threading.Tasks;
using Updog.Domain;
using Updog.Application;
using System.Data.Common;
using Dapper;

namespace Updog.Persistance {
    /// <summary>
    /// CRUD interface for managing user's in the database.
    /// </summary>
    public sealed class UserRepo : DatabaseRepo<User>, IUserRepo {
        #region Fields
        /// <summary>
        /// Mapper to convert the user to it's record.
        /// </summary>
        private IUserRecordMapper userMapper;
        #endregion

        #region Constructor(s)
        public UserRepo(DbConnection connection) : base(connection) {
            this.userMapper = new UserRecordMapper();
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a user by their id.
        /// </summary>
        /// <param name="id">The id to look for.</param>
        /// <returns>The user with the id.</returns>
        public async Task<User?> FindById(int id) {
            return await Connection.QuerySingleOrDefaultAsync<User>(
                @"SELECT * FROM ""User"" WHERE Id = @Id;",
                new { Id = id }
            );
        }

        /// <summary>
        /// Find a user by their username.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <returns>The user with the username.</returns>
        public async Task<User?> FindByUsername(string username) {
            return await Connection.QueryFirstOrDefaultAsync<User>(
                @"SELECT * FROM ""User"" WHERE LOWER(Username) = LOWER(@Username);",
                new { Username = username }
            );
        }

        /// <summary>
        /// Find a user via their contact email.
        /// </summary>
        /// <param name="email">The email to look for.</param>
        /// <returns>The user found (if any).</returns>
        public async Task<User?> FindByEmail(string email) {
            return await Connection.QueryFirstOrDefaultAsync<User>(
                @"SELECT * FROM ""User"" WHERE LOWER(Email) = LOWER(@Email);",
                new { Email = email }
            );
        }

        /// <summary>
        /// Add a new user to the database.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public async Task Add(User user) {
            user.Id = await Connection.QueryFirstOrDefaultAsync<int>(
                @"INSERT INTO ""User"" (Username, Email, PasswordHash, JoinedDate) VALUES (@Username, @Email, @PasswordHash, @JoinedDate) RETURNING Id;",
                userMapper.Reverse(user)
            );
        }

        /// <summary>
        /// Update a user in the database.
        /// </summary>
        /// <param name="user">The user to update.</param>
        public async Task Update(User user) {
            await Connection.ExecuteAsync(
                @"UPDATE ""User"" SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, JoinedDate = @JoinedDate WHERE Id = @Id;",
                userMapper.Reverse(user)
            );
        }

        /// <summary>
        /// Delete a user from the database.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        public async Task Delete(User user) {
            await Connection.ExecuteAsync(
                @"DELETE FROM ""User"" WHERE Id = @Id;",
                userMapper.Reverse(user)
            );
        }
        #endregion
    }
}