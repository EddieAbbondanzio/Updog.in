/**
 * Information about a paged result set from the backend.
 */
export class PaginationInfo {
    /**
     * Create a new set of pagination information.
     * @param pageNumber The current page number.
     * @param pageSize The size of the page.
     * @param totalCount The total record count available.
     */
    constructor(public pageNumber: number, public pageSize: number, public totalCount: number) {}

    /**
     * Check to see if the paged result set has another one that can be
     * retrieved.
     */
    public hasNextPage(): boolean {
        return this.totalCount > this.pageSize * (this.pageNumber + 1);
    }

    /**
     * Check to see if the paged result set has a page previous to this one.
     */
    public hasPreviousPage(): boolean {
        return this.pageNumber > 0;
    }

    public pageCount(): number {
        return this.totalCount / this.pageSize;
    }
}
