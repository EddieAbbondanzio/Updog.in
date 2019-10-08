namespace Updog.Domain {
    public static class RegexPattern {
        /// <summary>
        /// Regex for URL safe identifiers.
        /// </summary>
        public const string UrlSafe = @"^[\w-]+$";

        public const string UrlProtocol = @"^(http://|https://)";
    }
}