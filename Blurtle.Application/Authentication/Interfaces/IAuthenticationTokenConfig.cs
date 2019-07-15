namespace Blurtle.Infrastructure {
    /// <summary>
    /// Configuration settings for the auth token handler.
    /// </summary>
    public interface IAuthenticationTokenConfig {
        #region Properties
        /// <summary>
        /// The secret key to sign tokens with.
        /// </summary>
        string Secret { get; }

        /// <summary>
        /// The number of seconds the token is valid for.
        /// </summary>
        uint Expires { get; }

        /// <summary>
        /// The issuer who gives out the token.
        /// </summary>
        /// <value></value>
        string Issuer { get; }
        #endregion
    }
}