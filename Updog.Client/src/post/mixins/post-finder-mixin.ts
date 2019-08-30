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
        return this.postModule.findById(id);
    }

    /**
     * Find a set of new posts.
     * @param paging The paging info.
     */
    public async $findPostsByNew(paging: PaginationParams) {
        return this.postModule.findByNew(paging);
    }

    /**
     * Find a set of posts for a user.
     * @param params The input parameters.
     */
    public async $findPostsByUser(params: PostFinderByUserParams) {
        return this.postModule.findByUser(params);
    }
}
