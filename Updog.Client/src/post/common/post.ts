import { PostType } from './post-type';
import { User } from '@/user/common/user';
import { PaginationInfo } from '@/core/pagination-info';

/**
 * Post made by a user. Probably a repost...
 */
export class Post {
    /**
     * Default number of posts in a page.
     */
    public static DEFAULT_PAGE_SIZE: number = 20;

    /**
     * Max char count for the title.
     */
    public static TITLE_MAX_LENGTH: number = 300;

    /**
     * Max char count for the body.
     */
    public static BODY_MAX_LENGTH: number = 10_000;

    /**
     * Create a new post.
     * @param id The unique ID of the post.
     * @param type THe type of post it is.
     * @param title The title of the post.
     * @param body The body of the post.
     * @param user The user that posted the post.
     * @param creationDate Date and time of posting.
     * @param commentCount The number of comments on it.
     */
    constructor(
        public id: number,
        public type: PostType,
        public title: string,
        public body: string,
        public user: User,
        public creationDate: Date,
        public commentCount: number
    ) {}
}
