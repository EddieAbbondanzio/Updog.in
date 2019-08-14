/**
 * Pagination information to retrieve a specific page of data.
 */
export class PaginationInfo {
    /**
     * Create a new set of pagination info.
     * @param pageNumber The number of the page.
     * @param pageSize The size of the page.
     */
    constructor(public pageNumber: number, public pageSize: number) {}
}
