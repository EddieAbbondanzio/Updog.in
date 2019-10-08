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
        public User CreateFromRegistration(UserRegistration registration) => new User() {
            Username = registration.Username,
            PasswordHash = passwordHasher.Hash(registration.Password),
            Email = StringUtils.NullifyWhiteSpace(registration.Email),
            JoinedDate = System.DateTime.UtcNow
        };

        public User CreateFromAdminConfig(IAdminConfig config) => new User() {
            Username = config.Username,
            PasswordHash = passwordHasher.Hash(config.Password),
            JoinedDate = System.DateTime.UtcNow
        };
    }
}