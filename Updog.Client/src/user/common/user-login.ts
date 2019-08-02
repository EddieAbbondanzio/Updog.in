import { User } from './user';

/**
 * Login information sent back by the backend.
 */
export class UserLogin {
    /**
     * The user the login belongs to.
     */
    public user: User;

    /**
     * The auth token issued by the back end.
     */
    public authToken: string;

    /**
     * Create a new user login.
     * @param user The user of the login.
     * @param authToken Their auth token (JWT).
     */
    constructor(user: User, authToken: string) {
        this.user = user;
        this.authToken = authToken;
    }
}
