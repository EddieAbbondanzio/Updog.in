<template>
    <div>
        <post-summary-list :posts="posts" />
        <pagination-nav
            v-if="posts != null && posts.length > 0"
            :pagination="posts.pagination"
            @previous="onPrevious"
            @next="onNext"
        />
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Watch } from 'vue-property-decorator';
import { Post } from '@/post/domain/post';
import { Space as SpaceEntity } from '@/space/domain/space';
import SpaceViewerMixin from '@/space/mixins/space-viewer-mixin';
import PostSummaryList from '@/post/ui/components/post-summary-list.vue';
import PaginationNav from '@/core/ui/components/pagination-nav.vue';
import { PaginationParams } from '@/core/pagination/pagination-params';
import { PostFindBySpaceParams } from '@/post/interactors/find-by-space/post-find-by-space-params';
import { PagedResultSet } from '@/core/pagination/paged-result-set';

/**
 * Page to view a space and it's posts.
 */
@Component({
    name: 'space',
    components: {
        PostSummaryList,
        PaginationNav
    }
})
export default class Space extends SpaceViewerMixin {
    public space: SpaceEntity | null = null;

    public posts: PagedResultSet<Post> | null = null;

    public async created() {
        this.refreshPosts();
    }

    @Watch('$route')
    public async watchRoute() {
        this.refreshPosts();
    }

    private async refreshPosts() {
        this.space = await this.$findSpace(this.$route.params.spaceName);
        this.posts = await this.$findPosts(
            new PostFindBySpaceParams(this.space.name, new PaginationParams(this.currentPage, Post.DEFAULT_PAGE_SIZE))
        );
    }

    public currentPage: number = 0;

    public async onPrevious() {
        this.currentPage--;
        this.refreshPosts();
    }

    public async onNext() {
        this.currentPage++;
        this.refreshPosts();
    }
}
</script>