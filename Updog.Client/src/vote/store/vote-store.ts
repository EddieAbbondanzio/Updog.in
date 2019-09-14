import { Module, VuexModule, Mutation, Action } from 'vuex-module-decorators';
import { VoteOnCommentParams } from '../interactors/vote-on-comment/vote-on-comment-params';
import { VoteDirection } from '../domain/vote-direction';
import { PostVoter } from '../interactors/vote-on-post/post-voter';
import { VoteOnPostParams } from '../interactors/vote-on-post/vote-on-post-params';
import { CommentVoter } from '../interactors/vote-on-comment/comment-voter';
import { PostMutation } from '@/post/store/post-mutation';
import { CommentMutation } from '@/comment/store/comment-mutation';

/**
 * Module to manage votes.
 */
@Module({ namespaced: true, name: 'vote' })
export default class VoteStore extends VuexModule {
    /**
     * Vote on a post.
     * @param params The vote type to apply.
     */
    @Action({ rawError: true })
    public async voteOnPost(params: VoteOnPostParams) {
        const res = await new PostVoter(this.context.rootGetters['user/authToken']).handle(params);
        this.context.commit(`post/${PostMutation.Vote}`, params, { root: true });

        return res;
    }

    /**
     * Vote on a comment.
     * @param params The vote params..
     */
    @Action({ rawError: true })
    public async voteOnComment(params: VoteOnCommentParams) {
        const res = await new CommentVoter(this.context.rootGetters['user/authToken']).handle(params);
        this.context.commit(`comment/${CommentMutation.Vote}`, params, { root: true });
        return res;
    }
}
