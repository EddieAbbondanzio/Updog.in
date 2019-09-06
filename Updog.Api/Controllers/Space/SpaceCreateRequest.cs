namespace Updog.Api {
    /// <summary>
    /// An incoming request to create a new space.
    /// </summary>
    public sealed class SpaceCreateRequest {
        #region Properties
        /// <summary>
        /// The desired name of the space.
        /// </summary>
        /// <value></value>
        public string Name { get; set; } = "";

        /// <summary>
        /// The text description of the space to create.
        /// </summary>
        public string Description { get; set; } = "";
        #endregion
    }
}