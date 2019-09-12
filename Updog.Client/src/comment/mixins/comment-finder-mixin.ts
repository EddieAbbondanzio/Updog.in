import Mixin from 'vue-class-component';
import Vue from 'vue';
import { CommentFinderById } from '../use-cases/find-by-id/comment-finder-by-id';
import { CommentFinderByPost } from '../use-cases/find-by-post/comment-finder-by-post';
import { CommentCreateParams } from '../use-cases/create/comment-create-params';
import { CommentCreator } from '../use-cases/create/comment-creator';
import { Comment } from '@/comment/domain/comment';
import { PaginationParams } from '@/core/pagination/pagination-params';
import { CommentFinderByUser } from '../use-cases/find-by-user/comment-finder-by-user';
import { CommentFinderByUserParams } from '../use-cases/find-by-user/comment-finder-by-user-params';
import { PagedResultSet } from '@/core/pagination/paged-result-set';
import { CommentFinderByPostParams } from '../use-cases/find-by-post/comment-finder-by-post-params';
import { getModule } from 'vuex-module-decorators';
import CommentModule from '../store/comment-module';

/**
 * Mixin to handle comment related things.
 */
@Mixin
export class CommentFinderMixin extends Vue {
    get $cachedComments() {
        const module = getModule(CommentModule, this.$store);
        return module.comments;
    }

    /**
     * Find a post by it's unique ID.
     * @param request The ID of the post to retrieve.
     */
    public async $findCommentById(request: number) {
        const module = getModule(CommentModule, this.$store);
        return module.findById(request);
    }

    /**
     * Find all of the comments for a post.
     * @param params The info of the post to get comments for.
     */
    public async $findCommentsByPost(params: CommentFinderByPostParams) {
        const module = getModule(CommentModule, this.$store);
        return module.findByPost(params);
    }

    /**
     * Find a list of comments made by a user.
     * @param username The username of the user.
     * @param paginationInfo Pagination info.
     */
    public async $findCommentsByUser(
        username: string,
        paginationInfo: PaginationParams
    ): Promise<PagedResultSet<Comment>> {
        const module = getModule(CommentModule, this.$store);
        return module.findByUser(new CommentFinderByUserParams(username, paginationInfo));
    }
}
