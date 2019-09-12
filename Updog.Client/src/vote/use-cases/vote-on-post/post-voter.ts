import { VoteApiInteractor } from '@/vote/infrastructure/vote-api-interactor';
import { Vote } from '@/vote/domain/vote';
import { VoteOnPostParams } from './vote-on-post-params';

/**
 * Interactor to vote on a post.
 */
export class PostVoter extends VoteApiInteractor<VoteOnPostParams, Vote> {
    public async handle(input: VoteOnPostParams): Promise<Vote> {
        const response = await this.http.post(`/vote/${input.postId}/${input.vote}`);
        return this.voteMapper.map(response.data);
    }
}
