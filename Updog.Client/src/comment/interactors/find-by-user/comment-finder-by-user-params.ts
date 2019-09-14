import { PaginationParams } from '@/core/pagination/pagination-params';

/**
 * Params for the comment finder by user interactor.
 */
export class CommentFinderByUserParams {
    /**
     * Create a new set of parameters for the post finder by user interactor.
     * @param username The username of the user to get comments for.
     * @param paginationInfo The paging info to send over to the back end.
     */
    constructor(public username: string, public paginationInfo: PaginationParams) {}
}
