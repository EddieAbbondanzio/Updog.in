import { VoteApiInteractor } from '@/vote/infrastructure/vote-api-interactor';
import { VoteOnCommentParams } from './vote-on-comment-params';
import { Vote } from '@/vote/domain/vote';

/**
 * Interactor to vote on a comment.
 */
export class CommentVoter extends VoteApiInteractor<VoteOnCommentParams, Vote> {
    public async handle(input: VoteOnCommentParams): Promise<Vote> {
        const response = await this.http.post(`/vote/${input.commentId}/${input.vote}`);
        return this.voteMapper.map(response.data);
    }
}
