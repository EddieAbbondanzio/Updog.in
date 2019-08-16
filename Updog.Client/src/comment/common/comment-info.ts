import { DateUtils } from '@/core/utils/date-utils';

/**
 * Information about a comment.
 */
export class CommentInfo {
    /**
     * Create a new post info.
     * @param id The ID of the post.
     * @param author The author's name.
     * @param body The body (URL or text) of the post.
     * @param date The date of commenting.
     */
    public constructor(public id: number, public author: string, public body: string, public date: Date) {}

    /**
     * Get the time since it was commented in a user friendly
     * readable string.
     */
    public getDifferenceDate() {
        return DateUtils.getDifferenceFromToday(this.date);
    }
}
