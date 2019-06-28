namespace Blurtle.Domain {
    /// <summary>
    /// Base class for domain entities to implement.
    /// </summary>
    public abstract class Entity {
        #region Properties
        /// <summary>
        /// Unique integer id of the entity.
        /// </summary>
        public int Id { get; set; }
        #endregion
    }
}