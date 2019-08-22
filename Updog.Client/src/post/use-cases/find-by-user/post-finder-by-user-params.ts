import { PaginationInfo } from '@/core/pagination-info';

/**
 * Params for the post finder by user interactor.
 */
export class PostFinderByUserParams {
    /**
     * Create a new set of parameters for the post finder by user interactor.
     * @param username The username of the user to get posts for.
     * @param paginationInfo The paging info to send over to the back end.
     */
    constructor(public username: string, public paginationInfo: PaginationInfo) {}
}
