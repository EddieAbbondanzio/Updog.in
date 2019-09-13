import Mixin from 'vue-class-component';
import Vue from 'vue';
import { VoteDirection } from '../domain/vote-direction';
import { getModule } from 'vuex-module-decorators';
import VoteModule from '../store/vote-module';
import { Vote } from '../domain/vote';
import { UserAuthMixin } from '@/user/mixins/user-auth-mixin';
import { VoteOnCommentParams } from '../use-cases/vote-on-comment/vote-on-comment-params';
import { VoteOnPostParams } from '../use-cases/vote-on-post/vote-on-post-params';

/**
 * Mixin to handle voting on comments..
 */
@Mixin
export class CommentVoterMixin extends UserAuthMixin {
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
