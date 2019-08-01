/**
 * Username password combo to log in a user.
 */
export class UserCredentials {
    /**
     * Create a new set of user credentials.
     * @param username The username to auth under.
     * @param password The password to auth with.
     */
    constructor(public username: string, public password: string) {}
}
