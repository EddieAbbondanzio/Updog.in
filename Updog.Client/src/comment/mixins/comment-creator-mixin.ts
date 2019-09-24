import Mixin from 'vue-class-component';
import Vue from 'vue';
import { CommentCreateParams } from '../interactors/create/comment-create-params';
import CommentStore from '../store/comment-store';
import { getModule } from 'vuex-module-decorators';
import { AuthenticatedMixin } from '@/user';
import { CommentMutation } from '../store/comment-mutation';

/**
 * Mixin to handle comment related things.
 */
@Mixin
export class CommentCreatorMixin extends AuthenticatedMixin {
    get $cachedCommentInProgress() {
        const commentModule = getModule(CommentStore, this.$store);
        return commentModule.commentInProgress;
    }

    /**
     * Create a new comment.
     * @param params The new comment info.
     */
    public async $createComment(params: CommentCreateParams) {
        const commentModule = getModule(CommentStore, this.$store);
        return commentModule.create(params);
    }

    public $cacheCommentInProgress(comment: string) {
        const commentModule = getModule(CommentStore, this.$store);
        commentModule[CommentMutation.SetCommentInProgress](comment);
    }

    public $clearCommentInProgress() {
        const commentModule = getModule(CommentStore, this.$store);
        commentModule[CommentMutation.SetCommentInProgress](null);
    }
}
