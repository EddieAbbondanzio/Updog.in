using Blurtle.Application;

namespace Blurtle.Infrastructure {
    /// <summary>
    /// BCrypt based password hasher.
    /// </summary>
    public sealed class BCryptPasswordHasher : IPasswordHasher {
        /// <summary>
        /// Hash a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hash.</returns>
        public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        /// <summary>
        /// Verify a password matches a hash.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="hash">The hash to compare against.</param>
        /// <returns>True if the password matches the hash.</returns>
        public bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}