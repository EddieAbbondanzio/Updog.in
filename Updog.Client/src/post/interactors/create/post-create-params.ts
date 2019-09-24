import { PostType } from '@/post/domain/post-type';

/**
 * Request to create a new post.
 */
export class PostCreateParams {
    /**
     *
     * @param space The space to submit to.
     * @param type The type of post it is.
     * @param title The title of the post.
     * @param body The body of the post.
     */
    constructor(public space: string, public type: PostType, public title: string, public body: string) {}
}
