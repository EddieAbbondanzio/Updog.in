import Mixin from 'vue-class-component';
import Vue from 'vue';
import { PostCreateParams } from '../use-cases/create/post-create-params';
import { Post } from '../common/post';
import { PostCreator } from '../use-cases/create/post-creator';
import PostModule from '../store/post-module';
import { getModule } from 'vuex-module-decorators';

/**
 * Mixin to handle creating new posts.
 */
@Mixin
export class PostCreatorMixin extends Vue {
    private postModule: PostModule = getModule(PostModule, this.$store);

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
        await this.postModule.create(request);
        return this.postModule.activePost!;
    }
}
