import { Post } from '@/post/common/post';

/**
 * Information to create a new comment.
 */
export class CommentCreateParams {
    /**
     *
     * @param body The text of the comment.
     * @param post The post being commented on.
     * @param parent The parent comment it's a child of. (If any).
     */
    constructor(public body: string, public postId: number, public parentId: number = 0) {}
}
