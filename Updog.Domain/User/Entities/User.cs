using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Updog.Domain {
    /// <summary>
    /// A user of the site.
    /// </summary>
    public partial class User : Entity<User>, IUpdatable<UserUpdate> {
        #region Constants
        /// <summary>
        /// Minimum number of characters in a password.
        /// </summary>
        public const int PasswordMinLength = 8;
        public const int UsernameMinLength = 4;
        public const int UsernameMaxLength = 24;
        public const int EmailMaxLength = 64;
        #endregion

        #region Properties
        public string Username { get; }
        public string? Email { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime JoinedDate { get; }
        public int PostKarma { get; private set; }
        public int CommentKarma { get; private set; }
        #endregion

        #region Fields
        private IPasswordHasher passwordHasher;
        #endregion

        #region Constructor(s)
        internal User(IPasswordHasher passwordHasher, string username, string passwordHash, string? email = null) {
            this.passwordHasher = passwordHasher;
            Username = username;
            PasswordHash = passwordHash;
            Email = email;
        }

        internal User(IPasswordHasher passwordHasher, int id, string username, string? email, string passwordHash, DateTime joinedDate, int postKarma, int commentKarma) {
            this.passwordHasher = passwordHasher;
            Id = id;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            JoinedDate = joinedDate;
            PostKarma = postKarma;
            CommentKarma = commentKarma;
        }
        #endregion

        #region Publics
        public void SetPassword(string currentPassword, string newPassword) {
            if (!passwordHasher.Verify(currentPassword, PasswordHash)) {
                throw new UnauthorizedAccessException();
            }

            PasswordHash = passwordHasher.Hash(newPassword);
        }

        public void ResetPassword(string newPassword) {
            PasswordHash = passwordHasher.Hash(newPassword);
        }

        public bool Authenticate(string password) => passwordHasher.Verify(password, PasswordHash);

        public void Update(UserUpdate update) {
            Email = update.Email;
        }
        #endregion
    }
}
