/**
 * A user of the website.
 */
export class User {
    /**
     * The current active user.
     */
    public static CURRENT?: User;

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
     * @param username Their display name.
     * @param joinedDate When they registered.
     * @param email The contact email (if any).
     */
    constructor(username: string, joinedDate: Date, email?: string) {
        this.username = username;
        this.joinedDate = joinedDate;
        this.email = email;
    }
}
