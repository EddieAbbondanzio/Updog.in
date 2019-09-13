/**
 * Helper functions for numbers.
 */
export class NumberUtils {
    /**
     * Format a number with a k and round it instead of showing
     * all those zeros.
     * @param num The number to format.
     */
    public static formatWithK(num: number) {
        if (Math.abs(num) <= 999) {
            return num.toString();
        } else {
            return `${((Math.sign(num) * Math.abs(num)) / 1000).toFixed(1)}k`;
        }
    }
}
