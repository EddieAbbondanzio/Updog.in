namespace Updog.Domain {
    public interface IUserLoginFactory : IFactory<UserLogin> {
        #region Publics
        UserLogin Create(User user);
        UserLogin Create(int id, int userId);
        #endregion
    }
}