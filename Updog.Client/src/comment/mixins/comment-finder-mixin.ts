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

/**
 * Mixin to handle comment related things.
 */
@Mixin
export class CommentFinderMixin extends Vue {
    /**
     * Find a post by it's unique ID.
     * @param request The ID of the post to retrieve.
     */
    public async $findCommentById(request: number): Promise<Comment> {
        return new CommentFinderById().handle(request);
    }

    /**
     * Find all of the comments for a post.
     * @param postId The ID of the post to get comments for.
     */
    public async $findCommentsByPost(postId: number): Promise<Comment[]> {
        return new CommentFinderByPost().handle(postId);
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
        return new CommentFinderByUser().handle(new CommentFinderByUserParams(username, paginationInfo));
    }

    // /**
    //  * Create a new comment.
    //  * @param params The new comment info.
    //  */
    // public async $createComment(params: CommentCreateParams): Promise<Comment> {
    //     return new CommentCreator().handle(params);
    // }
}
