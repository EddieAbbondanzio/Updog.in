import { VoteDirection } from '@/vote/domain/vote-direction';

/**
 * Parameters to vote on a comment.
 */
export class VoteOnCommentParams {
    /**
     * Create a new set of parameters to vote on a comment.
     * @param commentId The ID of the comment to vote on.
     * @param vote The type of vote.
     */
    constructor(public commentId: number, public vote: VoteDirection) {}
}
