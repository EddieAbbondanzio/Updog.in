import { User } from '@/user/common/user';

/**
 * A text comment attached to a post.
 */
export class Comment {
    /**
     * The maximum number of characters allowed in the comment body.
     */
    public static BODY_MAX_LENGTH = 10_000;

    /**
     *
     * @param id The ID of the comment.
     * @param user The user who posted it.
     * @param body The body (text) of the comment.
     * @param creationDate The date of commenting.
     * @param children The children (nested) comments.
     */
    constructor(
        public id: number,
        public user: User,
        public body: string,
        public creationDate: Date,
        public children: Comment[]
    ) {}
}
