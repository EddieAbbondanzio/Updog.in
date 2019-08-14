import { PostType } from './post-type';
import { DateUtils } from '@/core/utils/date-utils';

/**
 * Information about a post.
 */
export class PostInfo {
    /**
     * Create a new post info.
     * @param id The ID of the post.
     * @param type The type flag of the post.
     * @param title The title of the post.
     * @param body The body (URL or text) of the post.
     * @param author The author's name.
     * @param date The date of posting.
     */
    public constructor(
        public id: number,
        public type: PostType,
        public title: string,
        public body: string,
        public author: string,
        public date: Date
    ) {}

    /**
     * Get the time since it was posted in a user friendly
     * readable string.
     */
    public getDifferenceDate() {
        return DateUtils.getDifferenceFromToday(this.date);
    }
}
