import { Module, VuexModule, Mutation, Action } from 'vuex-module-decorators';
import { VoteOnCommentParams } from '../interactors/vote-on-comment/vote-on-comment-params';
import { VoteDirection } from '../domain/vote-direction';
import { PostVoter } from '../interactors/vote-on-post/post-voter';
import { VoteOnPostParams } from '../interactors/vote-on-post/vote-on-post-params';
import { CommentVoter } from '../interactors/vote-on-comment/comment-voter';
import { PostMutation } from '@/post/store/post-mutation';
import { CommentMutation } from '@/comment/store/comment-mutation';
import { StoreName } from '@/core';
import { VoteAction } from './vote-action';
import { StoreNamespace } from '@/core/store/store-namespace';
import { StoreUtils } from '@/core/store/store-utils';

/**
 * Module to manage votes.
 */
@Module({ namespaced: true, name: StoreNamespace.Vote })
export default class VoteStore extends VuexModule {
    private postVoteMutation = StoreUtils.buildMutation(StoreName.Post, PostMutation.Vote);
    private commentVoteMutation = StoreUtils.buildMutation(StoreName.Comment, CommentMutation.Vote);

    /**
     * Vote on a post.
     * @param params The vote type to apply.
     */
    @Action({ rawError: true })
    public async [VoteAction.VoteOnPost](params: VoteOnPostParams) {
        const res = await new PostVoter(this.context.rootGetters['user/authToken']).handle(params);

        this.context.commit(this.postVoteMutation, params, { root: true });

        return res;
    }

    /**
     * Vote on a comment.
     * @param params The vote params..
     */
    @Action({ rawError: true })
    public async [VoteAction.VoteOnComment](params: VoteOnCommentParams) {
        const res = await new CommentVoter(this.context.rootGetters['user/authToken']).handle(params);

        this.context.commit(this.commentVoteMutation, params, { root: true });

        return res;
    }
}
