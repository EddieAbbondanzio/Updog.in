import Mixin from 'vue-class-component';
import Vue from 'vue';
import { PostCreateParams } from '../use-cases/create/post-create-params';
import { Post } from '../common/post';
import { PostCreator } from '../use-cases/create/post-creator';

/**
 * Mixin to handle creating new posts.
 */
@Mixin
export class PostCreatorMixin extends Vue {
    /**
     * Reirect to the post topic page.
     * @param id The ID of the new post.
     */
    public async $redirectToPost(id: number) {
        this.$router.push(`/post/${id}`);
    }

    /**
     * Create a new post.
     * @param request The post creation details.
     */
    public async $createPost(request: PostCreateParams): Promise<Post> {
        return new PostCreator().handle(request);
    }

    // /**
    //  * Update a post with the backend.
    //  * @param request The post update params.
    //  */
    // public async $updatePost(request: PostUpdateParams): Promise<Post> {
    //     return new PostUpdater().handle(request);
    // }
}
