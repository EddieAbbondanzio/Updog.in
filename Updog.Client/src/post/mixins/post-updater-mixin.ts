import Mixin from 'vue-class-component';
import Vue from 'vue';
import { PostCreateParams } from '../interactors/create/post-create-params';
import { Post } from '../domain/post';
import { PostCreator } from '../interactors/create/post-creator';
import { PostUpdateParams } from '../interactors/update/post-update-params';
import { PostUpdater } from '../interactors/update/post-updater';
import PostModule from '../store/post-store';
import { getModule } from 'vuex-module-decorators';
import { AuthenticatedMixin } from '@/user';

/**
 * Mixin to handle updating posts.
 */
@Mixin
export class PostUpdaterMixin extends AuthenticatedMixin {
    /**
     * Reirect to the post topic page.
     * @param id The ID of the new post.
     */
    public async $redirectToPost(id: number) {
        this.$router.push(`/post/${id}`);
    }

    /**
     * Update a post with the backend.
     * @param request The post update params.
     */
    public async $updatePost(request: PostUpdateParams): Promise<Post> {
        const postModule: PostModule = getModule(PostModule, this.$store);
        return postModule.update(request);
    }

    public async $deletePost(post: Post) {
        const postModule: PostModule = getModule(PostModule, this.$store);
        return postModule.delete(post);
    }
}
