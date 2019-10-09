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
        public User CreateFromRegistration(UserRegistration registration) => new User(registration.Username, Hash(registration.Password), registration.Email);

        public User CreateFromAdminConfig(IAdminConfig config) => new User(config.Username, Hash(config.Password));
        #endregion

        #region Privates
        public string Hash(string password) => passwordHasher.Hash(password);
        #endregion
    }
}