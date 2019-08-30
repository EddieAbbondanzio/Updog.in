import Mixin from 'vue-class-component';
import Vue from 'vue';
import { PostCreateParams } from '../use-cases/create/post-create-params';
import { Post } from '../common/post';
import { PostCreator } from '../use-cases/create/post-creator';
import { PostUpdateParams } from '../use-cases/update/post-update-params';
import { PostUpdater } from '../use-cases/update/post-updater';
import PostModule from '../store/post-module';
import { getModule } from 'vuex-module-decorators';

/**
 * Mixin to handle updating posts.
 */
@Mixin
export class PostUpdaterMixin extends Vue {
    private postModule: PostModule = getModule(PostModule, this.$store);

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
        return this.postModule.update(request);
    }
}
