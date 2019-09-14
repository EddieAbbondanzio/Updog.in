/**
 * Params for the comment finder by user interactor.
 */
export class CommentFinderByPostParams {
    /**
     * Create a new set of parameters for the post finder by user interactor.
     * @param postId The ID of the post to get comments for.
     * @param paginationInfo The paging info to send over to the back end.
     */
    constructor(public postId: number) {}
}
