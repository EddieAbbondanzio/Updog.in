import Mixin from 'vue-class-component';
import Vue from 'vue';
import CommentModule from '../store/comment-module';
import { getModule } from 'vuex-module-decorators';
import { CommentUpdateParams } from '../use-cases/update/comment-update-params';

/**
 * Mixin to handle comment related things.
 */
@Mixin
export class CommentUpdaterMixin extends Vue {
    private commentModule: CommentModule = getModule(CommentModule, this.$store);

    /**
     * Create a new comment.
     * @param params The new comment info.
     */
    public async $updateComment(params: CommentUpdateParams) {
        return this.commentModule.update(params);
    }
}
