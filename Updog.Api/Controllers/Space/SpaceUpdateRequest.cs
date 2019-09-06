namespace Updog.Api {
    /// <summary>
    /// An incoming request to update a space.
    /// </summary>
    public sealed class SpaceUpdateRequest {
        #region Properties
        /// <summary>
        /// The new description of the space.
        /// </summary>
        public string Description { get; set; } = "";
        #endregion
    }
}