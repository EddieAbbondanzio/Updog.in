import Mixin from 'vue-class-component';
import Vue from 'vue';
import PostModule from '../store/post-store';
import { getModule } from 'vuex-module-decorators';
import { PaginationInfo } from '@/core/pagination/pagination-info';
import { PostFinderByUserParams } from '../interactors/find-by-user/post-finder-by-user-params';
import { PaginationParams } from '@/core/pagination/pagination-params';

@Mixin
export class PostFinderMixin extends Vue {
    /**
     * The currently loaded posts.
     */
    get $posts() {
        const postModule: PostModule = getModule(PostModule, this.$store);
        return postModule.posts;
    }

    /**
     * Find an active post.
     * @param id The post to look for.
     */
    public async $findPostById(id: number) {
        const postModule: PostModule = getModule(PostModule, this.$store);
        return postModule.findById(id);
    }

    /**
     * Find a set of new posts.
     * @param paging The paging info.
     */
    public async $findPostsByNew(paging: PaginationParams) {
        const postModule: PostModule = getModule(PostModule, this.$store);
        return postModule.findByNew(paging);
    }

    /**
     * Find a set of posts for a user.
     * @param params The input parameters.
     */
    public async $findPostsByUser(params: PostFinderByUserParams) {
        const postModule: PostModule = getModule(PostModule, this.$store);
        return postModule.findByUser(params);
    }
}
