import Mixin from 'vue-class-component';
import Vue from 'vue';
import { Space } from '../domain/space';
import { getModule } from 'vuex-module-decorators';
import SpaceModule from '../store/space-store';
import { PostFinderMixin } from '@/post/mixins/post-finder-mixin';
import { PaginationParams } from '@/core/pagination/pagination-params';
import PostModule from '@/post/store/post-store';
import { PostFindBySpaceParams } from '@/post/interactors/find-by-space/post-find-by-space-params';

/**
 * Mixin to view info about a space.
 */
@Mixin
export class SpaceViewerMixin extends Vue {
    /**
     * Find a space via it's name.
     * @param spaceName The name of the space to look for.
     */
    public async $findSpace(spaceName: string) {
        const spaceModule = getModule(SpaceModule, this.$store);
        return spaceModule.findSpace(spaceName);
    }

    public async $findPosts(params: PostFindBySpaceParams) {
        const postModule = getModule(PostModule, this.$store);
        return postModule.findBySpace(params);
    }
}
