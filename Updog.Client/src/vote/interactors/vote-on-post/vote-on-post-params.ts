import { VoteDirection } from '@/vote/domain/vote-direction';

/**
 * Parameters to vote on a post.
 */
export class VoteOnPostParams {
    /**
     * Create a new set of parameters to vote on a post.
     * @param postId The ID of the comment to vote on.
     * @param vote The type of vote.
     */
    constructor(public postId: number, public vote: VoteDirection) {}
}
