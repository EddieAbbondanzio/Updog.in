/**
 * Parameters to update a comment.
 */
export class CommentUpdateParams {
    /**
     * Create a new set of comment update parameters.
     * @param commentId The ID of the comment to update.
     * @param body The new body of the comment.
     */
    constructor(public commentId: number, public body: string) {}
}
