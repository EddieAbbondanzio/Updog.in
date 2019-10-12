using System;
using Updog.Domain;

namespace Updog.Domain {
    public interface IUserFactory : IFactory<User> {
        #region Publics
        User CreateFromRegistration(UserRegistration registration);
        User CreateFromAdminConfig(IAdminConfig config);
        User Create(int id, string username, string? email, string passwordHash, DateTime joinedDate, int postKarma, int commentCount);
        #endregion
    }
}