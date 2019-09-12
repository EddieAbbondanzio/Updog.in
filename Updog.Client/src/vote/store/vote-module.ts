import { Module, VuexModule, Mutation, Action } from 'vuex-module-decorators';
import { VoteOnCommentParams } from '../use-cases/vote-on-comment/vote-on-comment-params';
import { VoteDirection } from '../domain/vote-direction';
import { PostVoter } from '../use-cases/vote-on-post/post-voter';
import { VoteOnPostParams } from '../use-cases/vote-on-post/vote-on-post-params';
import { CommentVoter } from '../use-cases/vote-on-comment/comment-voter';

/**
 * Module to manage votes.
 */
@Module({ namespaced: true, name: 'vote' })
export default class VoteModule extends VuexModule {
    /**
     * Vote on a post.
     * @param postId The ID of the post to vote on.
     * @param vote The vote type to apply.
     */
    @Action
    public voteOnPost(postId: number, vote: VoteDirection) {
        return new PostVoter().handle(new VoteOnPostParams(postId, vote));
    }

    /**
     * Vote on a comment.
     * @param commentId The ID of the comment to vote on.
     * @param vote The vote type to apply.
     */
    @Action
    public voteOnComment(commentId: number, vote: VoteDirection) {
        return new CommentVoter().handle(new VoteOnCommentParams(commentId, vote));
    }
}
