import { VoteResourceType } from './vote-resource-type';
import { VoteDirection } from './vote-direction';

/**
 * An upvote, or downvote on a post or comment.
 */
export class Vote {
    /**
     * Create a new vote.
     * @param resouceType The type of resource being voted on.
     * @param resourceId The ID of the resource.
     * @param direction The direction of the vote.
     */
    public constructor(
        public resouceType: VoteResourceType,
        public resourceId: number,
        public direction: VoteDirection
    ) {}
}
