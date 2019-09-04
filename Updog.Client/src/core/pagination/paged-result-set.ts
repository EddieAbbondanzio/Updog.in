import { PaginationInfo } from './pagination-info';

/**
 * A result set from the backend that was paged.
 */
export class PagedResultSet<T> extends Array<T> {
    /**
     * The pagination info of the result set.
     */
    public pagination: PaginationInfo;

    /**
     * Create a new paged result set.
     * @param items The items of the result set.
     * @param pagination The pagination info.
     */
    constructor(items: T[], pagination: PaginationInfo) {
        super();
        items.forEach(i => this.push(i));
        this.pagination = pagination;
    }

    /**
     * Create a new empty pagination result set.
     */
    public static Empty<T>(): PagedResultSet<T> {
        return new PagedResultSet([], new PaginationInfo(0, 0, 0));
    }
}
