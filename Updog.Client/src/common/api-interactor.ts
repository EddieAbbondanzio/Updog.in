import axios, { AxiosInstance } from 'axios';

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

    constructor() {
        this.http = axios.create({ baseURL: ApiInteractor.BACKEND_URL });
    }

    /**
     * Invoke the back end.
     * @param input The input to process.
     */
    public abstract handle(input: TInput): Promise<TOutput>;
}
