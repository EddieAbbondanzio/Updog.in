using System;

namespace Updog.Domain {
    /// <summary>
    /// Utility methods for strings.
    /// </summary>
    public static class StringUtils {
        /// <summary>
        /// Nullify a string if it is empty, or whitespace.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns>Null if the string was whitespace.</returns>
        public static string? NullifyWhiteSpace(string? str) => String.IsNullOrWhiteSpace(str) ? null : str;
    }
}