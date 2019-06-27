using Blurtle.Infrastructure;

namespace Blurtle.Api {
    /// <summary>
    /// Configuration settings for the auth token handler.
    /// </summary>
    public sealed class AuthenticationTokenConfig : IAuthenticationTokenConfig {
        #region Properties
        /// <summary>
        /// The secret key to sign tokens with.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// The number of seconds the token is valid for.
        /// </summary>
        public uint Lifespan { get; set; }
        #endregion
    }
}