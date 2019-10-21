namespace Updog.Domain {
    public sealed class UserLoginFactory : IUserLoginFactory {
        #region Publics
        public UserLogin Create(User user) => new UserLogin(user.Id);
        public UserLogin Create(int id, int userId) => new UserLogin(id, userId);
        #endregion
    }
}