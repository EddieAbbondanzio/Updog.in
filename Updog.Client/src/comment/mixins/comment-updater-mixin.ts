import Mixin from 'vue-class-component';
import Vue from 'vue';
import CommentModule from '../store/comment-store';
import { getModule } from 'vuex-module-decorators';
import { CommentUpdateParams } from '@/comment';

/**
 * Mixin to handle comment related things.
 */
@Mixin
export class CommentUpdaterMixin extends Vue {
    /**
     * Create a new comment.
     * @param params The new comment info.
     */
    public async $updateComment(params: CommentUpdateParams) {
        const commentModule: CommentModule = getModule(CommentModule, this.$store);
        return await commentModule.update(params);
    }
}
