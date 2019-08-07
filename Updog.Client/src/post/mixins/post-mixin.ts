import Component from 'vue-class-component';
import Vue from 'vue';
import { PostCreateRequest } from '../common/post-create-request';
import { Post } from '../common/post';
import { PostCreator } from '../use-cases/post-creator';

export class PostMixin extends Vue {
    /**
     * Create a new post.
     * @param request The post creation details.
     */
    public async $createPost(request: PostCreateRequest): Promise<Post> {
        return new PostCreator().handle(request);
    }
}
