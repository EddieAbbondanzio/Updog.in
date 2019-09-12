import { User } from '@/user/domain/user';
import { UserEntity } from '@/core/common/user-entity';
import { Vote } from '@/vote/domain/vote';

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
     * The children on the comment.
     */
    public children: Comment[];

    /**
     *
     * @param id The ID of the comment.
     * @param user The user who posted it.
     * @param body The body (text) of the comment.
     * @param creationDate The date of commenting.
     * @param wasUpdated If the comment was updated.
     * @param wasDeleted If the comment was deleted.
     * @param children The children (nested) comments.
     * @param upvotes The number of upvotes it has.
     * @param downvotes The number of downvotes it has.
     * @param vote The current user's vote on it.
     */
    constructor(
        public id: number,
        public user: User,
        public body: string,
        public creationDate: Date,
        public wasUpdated: boolean,
        public wasDeleted: boolean,
        public upvotes: number,
        public downvotes: number,
        public vote: Vote | null = null
    ) {
        super();
        this.children = [];
    }
}
