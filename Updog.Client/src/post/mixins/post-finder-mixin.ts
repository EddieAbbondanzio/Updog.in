import Mixin from 'vue-class-component';
import Vue from 'vue';
import PostModule from '../store/post-module';
import { getModule } from 'vuex-module-decorators';
import { PaginationInfo } from '@/core/pagination/pagination-info';
import { PostFinderByUserParams } from '../use-cases/find-by-user/post-finder-by-user-params';
import { PaginationParams } from '@/core/pagination/pagination-params';

@Mixin
export class PostFinderMixin extends Vue {
    private postModule: PostModule = getModule(PostModule, this.$store);

    /**
     * The currently loaded posts.
     */
    get $posts() {
        return this.postModule.posts;
    }

    /**
     * Find an active post.
     * @param id The post to look for.
     */
    public async $findPostById(id: number) {
        await this.postModule.findById(id);
        return this.$posts != null ? this.$posts[0] : null;
    }

    /**
     * Find a set of new posts.
     * @param paging The paging info.
     */
    public async $findPostsByNew(paging: PaginationParams) {
        await this.postModule.findByNew(paging);
        return this.$posts;
    }

    /**
     * Find a set of posts for a user.
     * @param params The input parameters.
     */
    public async $findPostsByUser(params: PostFinderByUserParams) {
        await this.postModule.findByUser(params);
        return this.$posts;
    }
}
