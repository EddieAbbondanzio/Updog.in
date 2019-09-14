import Mixin from 'vue-class-component';
import Vue from 'vue';
import { CommentCreateParams } from '../interactors/create/comment-create-params';
import CommentStore from '../store/comment-store';
import { getModule } from 'vuex-module-decorators';

/**
 * Mixin to handle comment related things.
 */
@Mixin
export class CommentCreatorMixin extends Vue {
    /**
     * Create a new comment.
     * @param params The new comment info.
     */
    public async $createComment(params: CommentCreateParams) {
        const commentModule = getModule(CommentStore, this.$store);
        return commentModule.create(params);
    }
}
