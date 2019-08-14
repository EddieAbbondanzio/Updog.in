import moment from 'moment';

/**
 * Helper functions related to dates.
 */
export class DateUtils {
    /**
     * Get a human readable string of how long ago the date occured.
     * @param date The date to get.
     */
    public static getDifferenceFromToday(date: Date): string {
        const m1 = moment(date);
        const m2 = moment(new Date());

        return moment.duration(m1.diff(m2)).humanize();
    }
}
