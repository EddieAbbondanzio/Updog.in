import Mixin from 'vue-class-component';
import { VoteDirection } from '../domain/vote-direction';
import { getModule } from 'vuex-module-decorators';
import VoteModule from '../store/vote-store';
import { Vote } from '../domain/vote';
import { VoteOnCommentParams } from '../interactors/vote-on-comment/vote-on-comment-params';
import AuthenticatedMixin from '@/user/mixins/authenticated-mixin';

/**
 * Mixin to handle voting on comments..
 */
@Mixin
export default class CommentVoterMixin extends AuthenticatedMixin {
    /**
     * Vote on a post.
     * @param commentId The ID of the comment to vote on.
     * @param direction The way to vote.
     */
    public async $vote(commentId: number, direction: VoteDirection): Promise<Vote> {
        const voteModule = getModule(VoteModule, this.$store);
        return voteModule.voteOnComment(new VoteOnCommentParams(commentId, direction));
    }
}
