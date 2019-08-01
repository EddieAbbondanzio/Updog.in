/**
 * A user of the website.
 */
export class User {
    /**
     * The unique ID of the user.
     */
    public id: number;

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
     * @param id The unique ID of the user.
     * @param username Their display name.
     * @param joinedDate When they registered.
     * @param email The contact email (if any).
     */
    constructor(id: number, username: string, joinedDate: Date, email?: string) {
        this.id = id;
        this.username = username;
        this.joinedDate = joinedDate;
        this.email = email;
    }
}
