import axios, { AxiosInstance, AxiosResponse } from 'axios';
import { PaginationInfo } from './pagination/pagination-info';

/**
 * Base class for API endpoint wrappers to implement.
 */
export abstract class ApiInteractor<TInput, TOutput> {
    /**
     * base URL to the backend.
     */
    private static BACKEND_URL: string = process.env.VUE_APP_BACKEND_URL;

    /**
     * HTTP client for making HTTP requests over the network.
     */
    protected http: AxiosInstance;

    /**
     * Create a new API interactor.
     * @param baseUrl The base URL of the interactor.
     */
    constructor(baseUrl: string = '') {
        this.http = axios.create({
            baseURL: baseUrl === '' ? ApiInteractor.BACKEND_URL : `${ApiInteractor.BACKEND_URL}/${baseUrl}`
        });
    }

    /**
     * Invoke the back end.
     * @param input The input to process.
     */
    public abstract handle(input: TInput): Promise<TOutput>;

    /**
     * Get the pagination info from the Content-Range header.
     * @param response The response to pull the pagination info from.
     */
    protected getPaginationInfo(response: AxiosResponse): PaginationInfo {
        /*
         * Content-Range header is in format: '${START_INDEX}-${END_INDEX}/${TOTAL_COUNT}'
         * START_INDEX: 0-based position of the first record.
         * END_INDEX: 0-based position of the last record.
         * TOTAL_COUNT: The total number of resources available.
         */

        const rangeHeader = response.headers['content-range'];

        const splitHeader = rangeHeader.split('/');
        const total = Number.parseInt(splitHeader[1], 10);

        const splitStartEnd = splitHeader[0].split('-');
        const start = Number.parseInt(splitStartEnd[0], 10);
        const end = Number.parseInt(splitStartEnd[1], 10);

        const pageSize = end - start + 1;
        const pageNumber = Math.floor((start + 1) / pageSize);

        return new PaginationInfo(pageNumber, pageSize, total);
    }
}
