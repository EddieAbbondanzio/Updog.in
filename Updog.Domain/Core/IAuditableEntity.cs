namespace Updog.Domain {
    /*
    * If C# had better support for proxies, this would be a more solid idea.
    */

    public interface IAuditableEntity {
        #region Properties
        bool WasUpdated { get; }
        bool WasDeleted { get; }
        #endregion
    }
}