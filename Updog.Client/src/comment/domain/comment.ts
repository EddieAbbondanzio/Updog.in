import { VotableEntity } from '@/vote/common/votable-entity';
import { VoteResourceType } from '@/vote/domain/vote-resource-type';
import { User } from '@/user/domain/user';
import { Vote } from '@/vote/domain/vote';

/**
 * A text comment attached to a post.
 */
export class Comment extends VotableEntity {
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
     * The type of vote resource it is.
     */
    public voteResourceType: VoteResourceType = VoteResourceType.Comment;

    /**
     *
     * @param id The ID of the comment.
     * @param user The user who posted it.
     * @param postId The post it belongs to.
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
        public postId: number,
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

    /**
     * Recursive helper to find a child however nested it may be.
     * @param id The ID of the child comment to look for.
     */
    public findChild(id: number): Comment | null {
        const found = this.children.find(c => c.id === id);

        if (found != null) {
            return found;
        }

        for (const c of this.children) {
            const deeperFind = c.findChild(id);

            if (deeperFind != null) {
                return deeperFind;
            }
        }

        return null;
    }

    /**
     * Attempt to delete a child however deep it may be.
     * @param id The ID of the child to delete.
     */
    public deleteChild(id: number): boolean {
        const rootIndex = this.children.findIndex(c => c.id === id);

        if (rootIndex !== -1) {
            this.children.splice(rootIndex, 1);
            return true;
        }

        for (const c of this.children) {
            const wasDeleted = c.deleteChild(id);

            if (wasDeleted) {
                return true;
            }
        }

        return false;
    }

    public childCount(): number {
        let count = this.children.length;

        for (const child of this.children) {
            count += child.childCount();
        }

        return count;
    }
}
