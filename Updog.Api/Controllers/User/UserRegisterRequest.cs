using System.ComponentModel;
using Newtonsoft.Json;

namespace Updog.Api {
    /// <summary>
    /// Request body to register a new user.
    /// </summary>
    public sealed class UserRegisterRequest {
        #region Properties
        /// <summary>
        /// The username they want.
        /// </summary>
        /// <value></value>
        public string Username { get; set; }

        /// <summary>
        /// The desired password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The contact email of the registration.
        /// </summary>
        [DefaultValue("")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Email { get; set; }
        #endregion
    }
}