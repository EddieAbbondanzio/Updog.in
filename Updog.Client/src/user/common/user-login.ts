import { User } from './user';

/**
 * Login information sent back by the backend.
 */
export class UserLogin {
    /**
     * Create a new user login.
     * @param user The user of the login.
     * @param authToken Their auth token (JWT).
     */
    constructor(public user: User, public authToken: string) {}
}
