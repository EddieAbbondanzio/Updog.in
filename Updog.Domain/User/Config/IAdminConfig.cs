namespace Updog.Domain {
    public interface IAdminConfig {
        #region Properties
        string Username { get; }

        string Password { get; }
        #endregion
    }
}