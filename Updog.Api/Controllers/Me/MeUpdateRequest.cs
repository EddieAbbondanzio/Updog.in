namespace Updog.Api {
    /// <summary>
    /// Request in the "Me" controller to update a user's info.
    /// </summary>
    public sealed class MeUpdateRequest {
        #region Publics
        /// <summary>
        /// The new contact email.
        /// </summary>
        /// <value></value>
        public string Email { get; set; } = "";
        #endregion
    }
}