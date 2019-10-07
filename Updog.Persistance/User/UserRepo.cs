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
        private IUserMapper mapper;
        #endregion

        #region Constructor(s)
        public UserRepo(IDatabase database, IUserMapper mapper) : base(database) {
            this.mapper = mapper;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a user by their id.
        /// </summary>
        /// <param name="id">The id to look for.</param>
        /// <returns>The user with the id.</returns>
        public override async Task<User?> FindById(int id) {
            var user = await Connection.QuerySingleOrDefaultAsync<UserRecord>(
                @"SELECT * FROM ""User"" WHERE Id = @Id;",
                new { Id = id }
            );

            return user != null ? mapper.Map(user) : null;
        }

        /// <summary>
        /// Find a user by their username.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <returns>The user with the username.</returns>
        public async Task<User?> FindByUsername(string username) {
            var user = await Connection.QueryFirstOrDefaultAsync<UserRecord>(
                @"SELECT * FROM ""User"" WHERE LOWER(Username) = LOWER(@Username);",
                new { Username = username }
            );

            return user != null ? mapper.Map(user) : null;
        }

        /// <summary>
        /// Find a user via their contact email.
        /// </summary>
        /// <param name="email">The email to look for.</param>
        /// <returns>The user found (if any).</returns>
        public async Task<User?> FindByEmail(string email) {
            var user = await Connection.QueryFirstOrDefaultAsync<UserRecord>(
                @"SELECT * FROM ""User"" WHERE LOWER(Email) = LOWER(@Email);",
                new { Email = email }
            );

            return user != null ? mapper.Map(user) : null;
        }

        /// <summary>
        /// Add a new user to the database.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public override async Task Add(User user) {
            user.Id = await Connection.QueryFirstOrDefaultAsync<int>(
                @"INSERT INTO ""User"" (Username, Email, PasswordHash, JoinedDate, PostKarma, CommentKarma) VALUES (@Username, @Email, @PasswordHash, @JoinedDate, @PostKarma, @CommentKarma) RETURNING Id;",
                mapper.Reverse(user)
            );
        }

        /// <summary>
        /// Update a user in the database.
        /// </summary>
        /// <param name="user">The user to update.</param>
        public override async Task Update(User user) {
            await Connection.ExecuteAsync(
                @"UPDATE ""User"" SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, JoinedDate = @JoinedDate, PostKarma = @PostKarma, CommentKarma = @CommentKarma WHERE Id = @Id;",
                mapper.Reverse(user)
            );
        }

        /// <summary>
        /// Delete a user from the database.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        public override async Task Delete(User user) {
            await Connection.ExecuteAsync(
                @"DELETE FROM ""User"" WHERE Id = @Id;",
                mapper.Reverse(user)
            );
        }
        #endregion
    }
}