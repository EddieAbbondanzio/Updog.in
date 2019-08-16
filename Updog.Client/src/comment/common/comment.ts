/**
 * A text comment attached to a post.
 */
export class Comment {
    /**
     * Create a new comment.
     * @param id The unique ID of the comment.
     * @param userId The user that created the comment.
     * @param postId The post the comment belongs to.
     * @param parentId The parent comment ID (if any).
     * @param body The text of the comment.
     */
    constructor(
        public id: number,
        public userId: number,
        public postId: number,
        public parentId: number,
        public body: string
    ) {}
}
