/**
 * Utility methods for URLs.
 */
export class UrlUtils {
    /**
     * Get the domain name and TLD from an URL.
     * @param url The URL to parse.
     */
    public static getHostName(url: string): string {
        const link = document.createElement('a');
        link.href = url;
        return link.hostname;
    }
}
