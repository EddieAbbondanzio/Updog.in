using System;
using Updog.Domain;

namespace Updog.Domain {
    public sealed class UserFactory : IUserFactory {
        #region Fields
        private IPasswordHasher passwordHasher;
        #endregion

        #region Constructor(s)
        public UserFactory(IPasswordHasher passwordHasher) {
            this.passwordHasher = passwordHasher;
        }
        #endregion

        #region Publics
        public User CreateFromRegistration(UserRegistration registration) => new User(passwordHasher, registration.Username, Hash(registration.Password), registration.Email);

        public User CreateFromAdminConfig(IAdminConfig config) => new User(passwordHasher, config.Username, Hash(config.Password));

        public User Create(int id, string username, string? email, string passwordHash, DateTime joinedDate, int postKarma, int commentCount) {
            User u = new User(passwordHasher, username, passwordHash, email);
            u.Id = id;

            return u;
        }
        #endregion

        #region Privates
        public string Hash(string password) => passwordHasher.Hash(password);
        #endregion
    }
}