import { PostType } from '@/post/domain/post-type';

/**
 * Request to create a new post.
 */
export class PostCreateParams {
    /**
     *
     * @param type The type of post it is.
     * @param title The title of the post.
     * @param body The body of the post.
     */
    constructor(public type: PostType, public title: string, public body: string) {}
}
