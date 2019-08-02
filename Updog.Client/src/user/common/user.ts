/**
 * A user of the website.
 */
export class User {
    /**
     * The auth token of the user.
     */
    public authToken: string;

    /**
     * The public display name of the user.
     */
    public username: string;

    /**
     * The contact email of the user.
     */
    public email?: string;

    /**
     * The date the user registered with the site.
     */
    public joinedDate: Date;

    /**
     * Create a new user.
     * @param authToken The unique ID.
     * @param username Their display name.
     * @param joinedDate When they registered.
     * @param email The contact email (if any).
     */
    constructor(authToken: string, username: string, joinedDate: Date, email?: string) {
        this.authToken = authToken;
        this.username = username;
        this.joinedDate = joinedDate;
        this.email = email;
    }
}
