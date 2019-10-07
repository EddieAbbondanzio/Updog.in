namespace Updog.Domain {
    /// <summary>
    /// Information used to connect to a database.
    /// </summary>
    public interface IDatabaseConfig {
        #region Properties
        /// <summary>
        /// The IP address.
        /// </summary>
        /// <value></value>
        string Host { get; set; }

        /// <summary>
        /// The port number of the IP.
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// The username to authenticate under.
        /// </summary>
        string User { get; set; }

        /// <summary>
        /// The secret password for authentication.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// The name of the database.
        /// </summary>
        string Database { get; set; }
        #endregion
    }
}