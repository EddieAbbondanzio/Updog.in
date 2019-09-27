import Mixin from 'vue-class-component';
import CommentModule from '../store/comment-store';
import { getModule } from 'vuex-module-decorators';
import { Comment } from '../domain/comment';
import AuthenticatedMixin from '@/user/mixins/authenticated-mixin';
import { CommentUpdateParams } from '../interactors/update/comment-update-params';

/**
 * Mixin to handle comment related things.
 */
@Mixin
export default class CommentUpdaterMixin extends AuthenticatedMixin {
    /**
     * Create a new comment.
     * @param params The new comment info.
     */
    public async $updateComment(params: CommentUpdateParams) {
        const commentModule: CommentModule = getModule(CommentModule, this.$store);
        return await commentModule.update(params);
    }

    /**
     * Delete an existing comment made by the user.
     * @param comment The comment to delete.
     */
    public async $deleteComment(comment: Comment) {
        const commentModule: CommentModule = getModule(CommentModule, this.$store);
        return await commentModule.delete(comment);
    }
}
