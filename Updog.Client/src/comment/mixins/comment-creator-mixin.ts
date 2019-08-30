import Mixin from 'vue-class-component';
import Vue from 'vue';
import { CommentFinderById } from '../use-cases/find-by-id/comment-finder-by-id';
import { CommentFinderByPost } from '../use-cases/find-by-post/comment-finder-by-post';
import { CommentCreateParams } from '../use-cases/create/comment-create-params';
import { CommentCreator } from '../use-cases/create/comment-creator';
import { Comment } from '@/comment/common/comment';
import { PaginationParams } from '@/core/pagination/pagination-params';
import { CommentFinderByUser } from '../use-cases/find-by-user/comment-finder-by-user';
import { CommentFinderByUserParams } from '../use-cases/find-by-user/comment-finder-by-user-param';
import { PagedResultSet } from '@/core/pagination/paged-result-set';
import CommentModule from '../store/comment-module';
import { getModule } from 'vuex-module-decorators';

/**
 * Mixin to handle comment related things.
 */
@Mixin
export class CommentCreatorMixin extends Vue {
    private commentModule: CommentModule = getModule(CommentModule, this.$store);

    /**
     * Create a new comment.
     * @param params The new comment info.
     */
    public async $createComment(params: CommentCreateParams) {
        await this.commentModule.create(params);
        return this.commentModule.activeComment!;
    }
}
