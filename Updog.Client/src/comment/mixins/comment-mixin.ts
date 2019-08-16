import Component from 'vue-class-component';
import Vue from 'vue';
import { CommentFinderById } from '../use-cases/find-by-id/comment-finder-by-id';
import { CommentInfo } from '../common/comment-info';
import { CommentFinderByPost } from '../use-cases/find-by-post/comment-finder-by-post';

/**
 * Mixin to handle comment related things.
 */
export class CommentMixin extends Vue {
    /**
     * Find a post by it's unique ID.
     * @param request The ID of the post to retrieve.
     */
    public async $findCommentById(request: number): Promise<CommentInfo> {
        return new CommentFinderById().handle(request);
    }

    /**
     * Find all of the comments for a post.
     * @param postId The ID of the post to get comments for.
     */
    public async $findCommentsByPost(postId: number): Promise<CommentInfo[]> {
        return new CommentFinderByPost().handle(postId);
    }
}
