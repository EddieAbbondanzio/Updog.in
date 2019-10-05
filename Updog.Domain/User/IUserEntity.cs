namespace Updog.Domain {
    /// <summary>
    /// Interface for entities owned by users to implement.
    /// </summary>
    public interface IUserEntity : IEntity {
        #region Properties
        /// <summary>
        /// The user it belongs to.
        /// </summary>
        int UserId { get; }
        #endregion
    }
}