import { AxiosStatic } from 'axios';

/**
 * Base class for services to implement.
 */
export class ApiService {
    /**
     * base URL to the backend.
     */
    protected backendUrl: string = process.env.VUE_APP_BACKEND_URL;

    /**
     * HTTP client for making HTTP requests over the network.
     */
    protected http: AxiosStatic = require('axios');
}
