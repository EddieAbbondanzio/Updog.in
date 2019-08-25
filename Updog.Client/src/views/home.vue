<template>
    <master-page>
        <template v-if="posts != null">
            <post-summary v-for="post in posts" v-bind:key="post.id" :post="post" />
            <pagination-navigation
                :pagination="posts.pagination"
                @previous="onPrevious"
                @next="onNext"
            />
        </template>
        <template slot="side-bar">
            <div class="bg-light border p-3">
                <create-post-buttons />
            </div>
        </template>
    </master-page>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import CreatePostButtons from '@/post/components/create-post-buttons.vue';
import MasterPage from '@/core/components/master-page.vue';
import { PostMixin } from '../post/mixins/post-mixin';
import { PaginationParams } from '../core/pagination/pagination-params';
import PostSummary from '@/post/components/post-summary.vue';
import { Post } from '../post/common/post';
import { PagedResultSet } from '../core/pagination/paged-result-set';
import PaginationNavigation from '@/core/components/pagination-navigation.vue';

@Component({
    components: {
        CreatePostButtons,
        MasterPage,
        PostSummary,
        PaginationNavigation
    }
})
export default class Home extends PostMixin {
    public posts: PagedResultSet<Post> | null = null;

    public currentPage: number = 0;

    public async created() {
        this.posts = await this.$findPostsByNew(new PaginationParams(this.currentPage, 3));
    }

    public async onPrevious() {
        this.currentPage--;
        this.posts = await this.$findPostsByNew(new PaginationParams(this.currentPage, 3));
    }

    public async onNext() {
        this.currentPage++;
        this.posts = await this.$findPostsByNew(new PaginationParams(this.currentPage, 3));
    }
}
</script>
