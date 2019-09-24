import Mixin from 'vue-class-component';
import Vue from 'vue';
import { PostCreateParams } from '../interactors/create/post-create-params';
import { Post } from '../domain/post';
import { PostCreator } from '../interactors/create/post-creator';
import PostModule from '../store/post-store';
import { getModule } from 'vuex-module-decorators';
import { SpaceFinderMixin } from '@/space';

/**
 * Mixin to handle creating new posts.
 */
@Mixin
export class PostCreatorMixin extends SpaceFinderMixin {
    /**
     * Reirect to the post topic page.
     * @param id The ID of the new post.
     */
    public async $redirectToPost(space: string, id: number) {
        this.$router.push({ name: 'post', params: { spaceName: space, postId: id.toString() } });
    }

    /**
     * Create a new post.
     * @param request The post creation details.
     */
    public async $createPost(request: PostCreateParams): Promise<Post> {
        const postModule: PostModule = getModule(PostModule, this.$store);
        return postModule.create(request);
    }
}
