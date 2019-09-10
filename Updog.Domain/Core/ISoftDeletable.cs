namespace Updog.Domain {
    /// <summary>
    /// An entity that can be deleted without actually deleting it.
    /// </summary>
    public interface ISoftDeletable {
        /// <summary>
        /// If the entity was deleted.
        /// </summary>
        /// <value></value>
        bool WasDeleted { get; set; }
    }
}