namespace Updog.Application {
    public interface IPasswordHasher {
        /// <summary>
        /// Hash a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hash.</returns>
        string Hash(string password);

        /// <summary>
        /// Verify that a password matches it's hash.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="hash">The hash to compare it against.</param>
        /// <returns>True if the password matches the hash.</returns>
        bool Verify(string password, string hash);
    }
}