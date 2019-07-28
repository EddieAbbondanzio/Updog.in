namespace Updog.Domain {
    /// <summary>
    /// Interface for domain entities to implement.
    /// </summary>
    public interface IEntity {
        #region Properties
        /// <summary>
        /// Unique integer id of the entity.
        /// </summary>
        int Id { get; set; }
        #endregion
    }
}