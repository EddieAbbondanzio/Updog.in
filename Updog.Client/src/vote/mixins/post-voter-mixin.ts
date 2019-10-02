import Mixin from 'vue-class-component';
import { VoteDirection } from '../domain/vote-direction';
import { getModule } from 'vuex-module-decorators';
import VoteModule from '../store/vote-store';
import { Vote } from '../domain/vote';
import { VoteOnPostParams } from '../interactors/vote-on-post/vote-on-post-params';
import AuthenticatedMixin from '@/user/mixins/authenticated-mixin';

/**
 * Mixin to handle voting on posts..
 */
@Mixin
export default class PostVoterMixin extends AuthenticatedMixin {
    /**
     * Vote on a post.
     * @param postId The ID of the post to vote on.
     * @param direction The way to vote.
     */
    public async $vote(postId: number, direction: VoteDirection): Promise<Vote> {
        const voteModule = getModule(VoteModule, this.$store);
        return voteModule.voteOnPost(new VoteOnPostParams(postId, direction));
    }
}
