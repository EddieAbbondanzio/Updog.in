using System;
using System.Threading.Tasks;
using Updog.Domain;
using Updog.Application;
using System.Data.Common;
using Dapper;
using System.Linq;

namespace Updog.Persistance {
    /// <summary>
    /// CRUD interface for managing user's in the database.
    /// </summary>
    public sealed class UserRepo : DatabaseRepo<User>, IUserRepo {
        #region Fields
        /// <summary>
        /// Mapper to convert the user to it's record.
        /// </summary>
        private IUserFactory factory;
        #endregion

        #region Constructor(s)
        public UserRepo(IDatabase database, IUserFactory factory) : base(database) {
            this.factory = factory;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Find a user by their id.
        /// </summary>
        /// <param name="id">The id to look for.</param>
        /// <returns>The user with the id.</returns>
        public override async Task<User?> FindById(int id) => (await Connection.QueryAsync<UserRecord>(
                @"SELECT * FROM ""User"" WHERE Id = @Id;",
                new {
                    Id = id
                }
        )).Select(u => Map(u)).FirstOrDefault();

        /// <summary>
        /// Find a user by their username.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <returns>The user with the username.</returns>
        public async Task<User?> FindByUsername(string username) => (await Connection.QueryAsync<UserRecord>(
                @"SELECT * FROM ""User"" WHERE LOWER(Username) = LOWER(@Username);",
                new { Username = username }
        )).Select(u => Map(u)).FirstOrDefault();

        /// <summary>
        /// Find a user via their contact email.
        /// </summary>
        /// <param name="email">The email to look for.</param>
        /// <returns>The user found (if any).</returns>
        public async Task<User?> FindByEmail(string email) => (await Connection.QueryAsync<UserRecord>(
            @"SELECT * FROM ""User"" WHERE LOWER(Email) = LOWER(@Email);",
            new { Email = email }
        )).Select(u => Map(u)).FirstOrDefault();


        /// <summary>
        /// Add a new user to the database.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public override async Task Add(User user) => user.Id = await Connection.QueryFirstOrDefaultAsync<int>(
                @"INSERT INTO ""User"" (Username, Email, PasswordHash, JoinedDate, PostKarma, CommentKarma) VALUES (@Username, @Email, @PasswordHash, @JoinedDate, @PostKarma, @CommentKarma) RETURNING Id;",
                Reverse(user)
            );

        /// <summary>
        /// Update a user in the database.
        /// </summary>
        /// <param name="user">The user to update.</param>
        public override async Task Update(User user) => await Connection.ExecuteAsync(
                @"UPDATE ""User"" SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, JoinedDate = @JoinedDate, PostKarma = @PostKarma, CommentKarma = @CommentKarma WHERE Id = @Id;",
                Reverse(user)
            );

        /// <summary>
        /// Delete a user from the database.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        public override async Task Delete(User user) => await Connection.ExecuteAsync(
                @"DELETE FROM ""User"" WHERE Id = @Id;",
                Reverse(user)
            );
        #endregion

        #region Privates
        private User Map(UserRecord rec) => factory.Create(rec.Id, rec.Username, rec.Email, rec.PasswordHash, rec.JoinedDate, rec.PostKarma, rec.CommentKarma);

        private UserRecord Reverse(User user) => new UserRecord() {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            JoinedDate = user.JoinedDate,
            PostKarma = user.PostKarma,
            CommentKarma = user.CommentKarma
        };
        #endregion
    }
}