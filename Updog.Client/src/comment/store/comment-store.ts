import { Module, VuexModule, Mutation, Action, getModule } from 'vuex-module-decorators';
import { CommentCreateParams } from '../interactors/create/comment-create-params';
import { CommentFinderByUserParams } from '../interactors/find-by-user/comment-finder-by-user-params';
import { CommentFinderById } from '../interactors/find-by-id/comment-finder-by-id';
import { CommentFinderByPost } from '../interactors/find-by-post/comment-finder-by-post';
import { CommentFinderByUser } from '../interactors/find-by-user/comment-finder-by-user';
import { CommentCreator } from '../interactors/create/comment-creator';
import { PagedResultSet } from '@/core/pagination/paged-result-set';
import { CommentMutation } from './comment-mutation';
import { PaginationInfo } from '@/core/pagination/pagination-info';
import { Comment } from '@/comment/domain/comment';
import { CommentUpdateParams } from '../interactors/update/comment-update-params';
import { CommentUpdater } from '../interactors/update/comment-updater';
import { CommentFinderByPostParams } from '../interactors/find-by-post/comment-finder-by-post-params';
import Vue from 'vue';
import { PostMutation } from '@/post/store/post-mutation';
import { VoteOnCommentParams } from '@/vote/interactors/vote-on-comment/vote-on-comment-params';
import { StoreName } from '@/core/store/store-name';
/**
 * Cache module for comments.
 */
@Module({ namespaced: true, name: StoreName.Comment })
export default class CommentStore extends VuexModule {
    public comments: Comment[] | null = null;

    @Mutation
    public [CommentMutation.SetComments](comments: Comment[]) {
        Vue.set(this, 'comments', comments);
    }

    @Mutation
    public [CommentMutation.ClearComments]() {
        Vue.set(this, 'comments', []);
    }

    @Mutation
    public [CommentMutation.Vote](params: VoteOnCommentParams) {
        const rootFound = this.comments!.find(c => c.id === params.commentId);

        if (rootFound != null) {
            rootFound.applyVote(params.vote);
        } else {
            // Gotta go deeper
            for (const comment of this.comments!) {
                const found = comment.findChild(params.commentId);

                if (found != null) {
                    found.applyVote(params.vote);
                    return;
                }
            }
        }
    }

    /**
     * Find a comment via it's ID.
     * @param id The comment's ID.
     */
    @Action
    public async findById(id: number) {
        return new CommentFinderById(this.context.rootGetters['user/authToken']).handle(id);
    }

    /**
     * Find a set of comments for a post.
     * @param params The post info to look for.
     */
    @Action({ rawError: true })
    public async findByPost(params: CommentFinderByPostParams) {
        this.context.commit(CommentMutation.ClearComments);
        const comments = await new CommentFinderByPost(this.context.rootGetters['user/authToken']).handle(params);
        this.context.commit(CommentMutation.SetComments, comments);

        return comments;
    }

    /**
     * Find a set comments for a specific user.
     * @param params Finder params..
     */
    @Action
    public async findByUser(params: CommentFinderByUserParams) {
        this.context.commit(CommentMutation.ClearComments);
        const comments = await new CommentFinderByUser(this.context.rootGetters['user/authToken']).handle(params);
        this.context.commit(CommentMutation.SetComments, comments);

        return comments;
    }

    /**
     * Create a new comemnt.
     * @param params The comment creation params.
     */
    @Action
    public async create(params: CommentCreateParams) {
        const c = await new CommentCreator(this.context.rootGetters['user/authToken']).handle(params);
        const all = [c, ...this.comments!];
        this.context.commit(CommentMutation.SetComments, all);
        this.context.commit(`post/${PostMutation.IncrementCommentCount}`, params.postId, { root: true });
        return c;
    }

    /**
     * Update an existing comment.
     * @param params The comment update params.
     */
    @Action({ rawError: true })
    public async update(params: CommentUpdateParams) {
        const updatedComment = await new CommentUpdater(this.context.rootGetters['user/authToken']).handle(params);

        // bleh
        const oldIndex = this.comments!.findIndex(c => c.id === params.commentId);
        const newComments = [...this.comments!];
        newComments.splice(oldIndex, 1, updatedComment);

        this.context.commit(CommentMutation.SetComments, newComments);

        return updatedComment;
    }
}
