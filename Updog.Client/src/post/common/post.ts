import { PostType } from './post-type';

/**
 * Post made by a user. Probably a repost...
 */
export class Post {
    /**
     * The unqiue ID of the post.
     */
    public id: number;

    /**
     * If the post is a link or text.
     */
    public type: PostType;

    /**
     * The eye catchy title of the post.
     */
    public title: string;

    /**
     * The body of the post. Either a link or text.
     */
    public body: string;

    /**
     * Create a new post.
     * @param id The unique ID of the post.
     * @param type THe type of post it is.
     * @param title The title of the post.
     * @param body The body of the post.
     */
    constructor(id: number, type: PostType, title: string, body: string) {
        this.id = id;
        this.type = type;
        this.title = title;
        this.body = body;
    }
}
