namespace Updog.Application {
    public static class Regex {
        /// <summary>
        /// Regex for URL safe identifiers.
        /// </summary>
        public const string UrlSafe = @"^[\w-]+$";

        public const string UrlProtocol = @"^(http://|https://)";
    }
}