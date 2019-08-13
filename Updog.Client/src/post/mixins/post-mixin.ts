import Component from 'vue-class-component';
import Vue from 'vue';
import { PostCreateParams } from '../common/post-create-params';
import { Post } from '../common/post';
import { PostCreator } from '../use-cases/post-creator';
import { PostFindByIdInteractor } from '../use-cases/post-find-by-id-interactor';

export class PostMixin extends Vue {
    /**
     * Create a new post.
     * @param request The post creation details.
     */
    public async $createPost(request: PostCreateParams): Promise<Post> {
        return new PostCreator().handle(request);
    }

    /**
     * Find a post by it's unique ID.
     * @param request The ID of the post to retrieve.
     */
    public async $findPostById(request: number): Promise<Post> {
        return new PostFindByIdInteractor().handle(request);
    }
}
