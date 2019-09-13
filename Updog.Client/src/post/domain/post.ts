import { PostType } from './post-type';
import { User } from '@/user/domain/user';
import { PaginationParams } from '@/core/pagination/pagination-params';
import { UserEntity } from '@/user/common/user-entity';
import { Vote } from '@/vote/domain/vote';
import { Space } from '@/space/domain/space';
import { VoteDirection } from '@/vote/domain/vote-direction';
import { VoteResourceType } from '@/vote/domain/vote-resource-type';
import { VotableEntity } from '@/vote/common/votable-entity';

/**
 * Post made by a user. Probably a repost...
 */
export class Post extends VotableEntity {
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
     * Flag to indicate what kind of votable entity it is.
     */
    public voteResourceType: VoteResourceType = VoteResourceType.Post;

    /**
     * Create a new post.
     * @param id The unique ID of the post.
     * @param type THe type of post it is.
     * @param title The title of the post.
     * @param body The body of the post.
     * @param user The user that posted the post.
     * @param space The space it was posted to.
     * @param creationDate Date and time of posting.
     * @param commentCount The number of comments on it.
     * @param wasUpdated If the post was editted.
     * @param wasDeleted If the post was deleted.
     * @param upvotes The number of upvotes it go.
     * @param downvotes The number of downvotes it has.
     * @param vote The vote cast by the current user.
     */
    constructor(
        public id: number,
        public type: PostType,
        public title: string,
        public body: string,
        public user: User,
        public space: Space,
        public creationDate: Date,
        public commentCount: number,
        public wasUpdated: boolean,
        public wasDeleted: boolean,
        public upvotes: number,
        public downvotes: number,
        public vote: Vote | null = null
    ) {
        super();
    }
}
