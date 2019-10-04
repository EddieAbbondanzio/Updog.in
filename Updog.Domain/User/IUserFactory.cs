using Updog.Domain;

namespace Updog.Domain {
    public interface IUserFactory : IFactory<User> {
        #region Publics
        User CreateFromRegistration(UserRegistration registration);
        User CreateFromAdminConfig(IAdminConfig config);
        #endregion
    }
}