/**
 * Registration info to create a new user.
 */
export class UserRegistration {
    /**
     * Create a new user registration.
     * @param username The username the user wants.
     * @param password The password they want to auth with.
     * @param email The contact email (if any).
     */
    constructor(public username: string, public password: string, public email?: string) {}
}
