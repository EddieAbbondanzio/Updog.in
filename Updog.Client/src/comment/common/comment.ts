import { User } from '@/user/common/user';
import { UserEntity } from '@/core/common/user-entity';

/**
 * A text comment attached to a post.
 */
export class Comment extends UserEntity {
    /**
     * The maximum number of characters allowed in the comment body.
     */
    public static BODY_MAX_LENGTH = 10_000;

    /**
     * The number of comments per page.
     */
    public static PAGE_SIZE = 10;

    /**
     *
     * @param id The ID of the comment.
     * @param user The user who posted it.
     * @param body The body (text) of the comment.
     * @param creationDate The date of commenting.
     * @param wasUpdated If the comment was updated.
     * @param wasDeleted If the comment was deleted.
     * @param children The children (nested) comments.
     */
    constructor(
        public id: number,
        public user: User,
        public body: string,
        public creationDate: Date,
        public wasUpdated: boolean,
        public wasDeleted: boolean,
        public children: Comment[]
    ) {
        super();
    }
}
