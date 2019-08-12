import Component from 'vue-class-component';
import Vue from 'vue';
import { PostCreateParams } from '../common/post-create-params';
import { Post } from '../common/post';
import { PostCreator } from '../use-cases/post-creator';

export class PostMixin extends Vue {
    /**
     * Create a new post.
     * @param request The post creation details.
     */
    public async $createPost(request: PostCreateParams): Promise<Post> {
        return new PostCreator().handle(request);
    }
}
